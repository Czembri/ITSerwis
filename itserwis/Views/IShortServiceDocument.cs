namespace ItSerwis_Merge_v2
{
    public interface IShortServiceDocument
    {
        int docID { get; set; }

        string GetDateString();
        void InitializeComponent();
    }
}