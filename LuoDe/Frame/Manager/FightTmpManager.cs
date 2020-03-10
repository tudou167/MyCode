

public class FightTmpManager:Singleton<FightTmpManager>
{
    FightTmpData data = null;
    public FightTmpData GetData(FightTmpData data = null)
    {
        if (data != null)
        {
            this.data = data;
        }
        else
        {
            if (this.data == null)
            {
                this.data = new FightTmpData();
                this.data.curPlieNum = 0;
                this.data.nNum = 0;
                this.data.rNum = 0;
                this.data.srNum = 0;
                this.data.ssrNum = 0;
                this.data.gold = 0;
                this.data.exp = 0;
                this.data.sword = null;
                this.data.ss = null;
                this.data.wand = null;
                this.data.consumable1 = null;
                this.data.consumable2 = null;
                this.data.consumable3 = null;
                this.data.isBack = false;
                this.data.isEnd = false;
            }
        }
        return this.data;
    }
}
