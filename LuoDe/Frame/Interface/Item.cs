using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    //用来背包查看的
    int ID { get;  }
    string Name { get;  }
    string Icon { get;  }
    int Price { get;  }
    string Detail { get;  }
    ItemQuality Quality { get;  }
    ItemType Type { get;  }
}
