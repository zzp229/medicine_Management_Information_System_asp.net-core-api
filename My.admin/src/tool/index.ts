import TagModel from "../class/TagModel";
import router from "../router";
import store from "../store";
import { getTreeMenu } from "../http";
import TreeModel from "../class/TreeModel";

// 选择菜单时添加tag
export const handleSelect = (index: string) => {
    if (index == "/") return;
    let name = router.getRoutes().filter(item => item.path == index)[0].name as string
    let model: TagModel = {
        Name: name,
        Index: index,
        Checked: false
    }
    let tags: Array<TagModel> = store().tags
    if (tags.find(p => p.Index == index) == undefined) {
        tags.push(model)
        store().$patch({
            tags: tags
        })
    }
    tagClick(index)
}
// 点击tag，设置checked，更新store，跳转路由
export const tagClick = (index: string) => {
    if (index == "/") return;
    let curr = store().tags
    curr.forEach(p => {
        if (p.Index == index) {
            p.Checked = true
        } else {
            p.Checked = false
        }
    })
    store().$patch({
        tags: curr
    })
    router.push({
        path: index
    })
}

//上面是给标签跳转用

// 递归路由，输出list
export const RecursiveRoutes = (tree: Array<TreeModel>) => {
    let list: Array<TreeModel> = [];
    for (let i = 0; i < tree.length; i++) {
        let node = tree[i];
        if (node.Children) {
            let childrenList = RecursiveRoutes(node.Children);
            list = list.concat(childrenList);
        }
        if (node.FilePath == '') {
            continue
        }
        list.push({
            Id: node.Id,
            Index: node.Index,
            Name: node.Name,
            FilePath: node.FilePath,
            Children: node.Children
        });
    }
    return list;
}

// 设置用户动态路由，更新全局状态
export const SettingUserRouter = async () => {

    //
    console.log("==========SettingUserRouter============")

    // 读取所有节点下的文件
    const m = import.meta.glob(['../views/*/*.vue', '../views/*/*/*.vue', '../views/*/*/*/*.vue'])
    console.log(m)
    let localArr: any[] = []
    for (var it in m) {
        localArr.push({ filepath: it, component: m[it] })
    }
    console.log(localArr)
    const obj = {
        Name: "",
        Index: "",
        FilePath: "",
        ParentId: "",
        Description: ""
    }
    // 对接权限菜单数据
    const tree: Array<TreeModel> = (await getTreeMenu(obj)) as any as Array<TreeModel>
    console.log(tree)
    // 递归路由，将tree转成list
    const list: Array<TreeModel> = RecursiveRoutes(tree)
    list.forEach(p => {
        // 动态添加路由
        router.addRoute("admin", {
            name: p.Name,
            path: p.Index,
            component: localArr.find(s => s.filepath.indexOf(p.FilePath) > -1).component
        })
    })

    // // 更新全局状态
    store().$patch({
        UserMenus: tree
    })
}