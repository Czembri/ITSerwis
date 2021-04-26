using System.Data;

namespace ItSerwis_Merge_v2
{
    public interface IServiceDocumentsAndDataSets
    {
        void DeleteServiceDocument(int id);
        DataSet fillDataset(string loaddatabindings, string sql, string nameOfDataSet);
        string GetLastDocumentID();
        ServiceDocumentsAndDataSets.ServiceDocumentOnRowClickValues GetServiceDocumentFromDatabase(int id);
        void InsertIntoClientsFromServiceDocs(string customerName, string customerLastName, string customerAddress, string date, int docID);
        void InsertIntoServiceDocuments(string date, string customerName, string customerLastName, string customerAddress, string empName, string empLastName, int empNum, string devType, string devBrand, string devModel, string descr, string documentnumber);
        void UpdateServiceDocument(int docID, string customerName, string customerLastName, string customerAddress, string empName, string empLastName, int empNum, string devType, string devBrand, string devModel, string descr);
    }
}