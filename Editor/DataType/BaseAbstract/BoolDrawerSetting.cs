namespace Meangpu.Datatype
{
    public struct BoolDrawerSetting
    {
        public string A_var_name;
        public string B_var_name;
        public int objectColumnSpace;
        public int boolTickSpace;
        public float offsetSize;

        public BoolDrawerSetting(string a_var_name, string b_var_name = "boolValue", int objectColumnSpace = 15, int boolTickSpace = 1, float offsetSize = 5)
        {
            A_var_name = a_var_name;
            B_var_name = b_var_name;
            this.objectColumnSpace = objectColumnSpace;
            this.boolTickSpace = boolTickSpace;
            this.offsetSize = offsetSize;
        }
    }
}