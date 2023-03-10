using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataGridInfo
/// </summary>
/// 

[Serializable]
public class DataGridInfo
{

    //ICE_Box ICE_Block ICE_Tag ICE_Order SN UID StockTime GroupName
        private string Model = "";
        private string ICE_Box = "";
        private string GroupName = "";
        private int ICE_Block = 0;
        private int ICE_Tag = 0;
        private int ICE_Order = 0;
        private string SN = "";
        private string UID = "";
        private string StockTime = "";

        public string ICE_Box1
        {
            get
            {
                return ICE_Box;
            }

            set
            {
                ICE_Box = value;
            }
        }

        public string GroupName1
        {
            get
            {
                return GroupName;
            }

            set
            {
                GroupName = value;
            }
        }

        

        public string SN1
        {
            get
            {
                return SN;
            }

            set
            {
                SN = value;
            }
        }

        public string UID1
        {
            get
            {
                return UID;
            }

            set
            {
                UID = value;
            }
        }

        public string StockTime1
        {
            get
            {
                return StockTime;
            }

            set
            {
                StockTime = value;
            }
        }

    public string Model1
    {
        get
        {
            return Model;
        }

        set
        {
            Model = value;
        }
    }

    public int ICE_Block1
    {
        get
        {
            return ICE_Block;
        }

        set
        {
            ICE_Block = value;
        }
    }

    public int ICE_Tag1
    {
        get
        {
            return ICE_Tag;
        }

        set
        {
            ICE_Tag = value;
        }
    }

    public int ICE_Order1
    {
        get
        {
            return ICE_Order;
        }

        set
        {
            ICE_Order = value;
        }
    }
}