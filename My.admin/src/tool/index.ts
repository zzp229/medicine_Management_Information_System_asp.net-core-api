import TagModel from "../class/TagModel";
import router from "../router";
import useStore from "../store";

// 选择菜单时添加tag
export const handleSelect = (index: string) => {
    if (index == "/") return;
    let name = router.getRoutes().filter(item => item.path == index)[0].name as string
    let model: TagModel = {
        Name: name,
        Index: index,
        Checked: false
    }
    let tags: Array<TagModel> = useStore().tags
    if (tags.find(p => p.Index == index) == undefined) {
        tags.push(model)
        useStore().$patch({
            tags: tags
        })
    }
    tagClick(index)
}
// 点击tag，设置checked，更新store，跳转路由
export const tagClick = (index: string) => {
    if (index == "/") return;
    let curr = useStore().tags
    curr.forEach(p => {
        if (p.Index == index) {
            p.Checked = true
        } else {
            p.Checked = false
        }
    })
    useStore().$patch({
        tags: curr
    })
    router.push({
        path: index
    })
}