using System;

namespace ItSerwis_Merge_v2
{
    public interface IProgram
    {
        void CreateShortServiceDocument(object sender, EventArgs e);
        void InitializeComponent();
        void Item(object sender, EventArgs e);
        void Logout(object sender, EventArgs e);
        void ShowServiceDocuments(object sender, EventArgs e);
    }
}