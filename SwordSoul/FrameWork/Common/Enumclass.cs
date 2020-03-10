
public class Enumclass
{
    public enum ItemType
    {
        // +武器
        weapon,
        // +帽子
        hat,
        // +衣服
        clothes,
        // +鞋子
        shoes,
        // +腰带
        belt,
        // +戒子
        ring,
        // +项链
        necklace,
        // +手镯
        bracelet,
        strengthen,//强化
        consumables,//消耗品
        task//任务
    }
    public enum Quality
    {
        普通,
        业余,
        稀有,
        专家,
        传奇
    }
    public enum TaskStatus
    {
        // +未接任务
        missed,
        // +完成任务
        complete,
        // +任务执行中
        executing,
        // +任务结算中
        settlement,
        // +任务失败
        failure,
        // +任务结束
        end
    }
}
