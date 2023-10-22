// 定义目录的接口
export default interface TreeModel {
    // 菜单路由地址
    Index: string
    // 菜单名称
    Name: string
    // 子级 数组：；类型+方括号
    // Children: TreeModel[]
    Children: Array<TreeModel>
    FilePath: string    //文件的路径
}