using System;
using System.Collections.Generic;
using System.Text;


namespace WpfApplication1
{
    class Profile
    {

        //head
        public string Name;
        public string Position;
        public string Birth;

        //contacts
        public string workPhone;
        public string mobilePhone;
        public string homePhone;
        public string skypeID;
        public string vkID;

        public void Add(string name, string pos, string birth, string wPhone, string mPhone, string hPhone, string sID, string vID)
        {
            Name = name;
            Position = pos;
            Birth = birth;
            workPhone = wPhone;
            mobilePhone = mPhone;
            homePhone = hPhone;
            skypeID = sID;
            vkID = vID;
        }
    }
}
