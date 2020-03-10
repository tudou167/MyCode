
public class EffectData
{
    //回收时间
    public int recyleTime;
    //范围
    public float range;
    //角度
    public int angleNum;
    public int damage;
    public float repel;
    public EffectData() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="recyleTime"></param>
    /// <param name="range"></param>
    /// <param name="angleNum"></param>
    /// <param name="damage"></param>
    /// <param name="repel"></param>
    public EffectData(int recyleTime, float range, int angleNum, int damage, float repel)
    {
        this.recyleTime = recyleTime;
        this.range = range;
        this.angleNum = angleNum;
        this.damage = damage;
        this.repel = repel;
    }
}
