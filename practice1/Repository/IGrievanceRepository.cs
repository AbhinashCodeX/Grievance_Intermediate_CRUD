using practice1.Models;

namespace practice1.Repository
{
    public interface IGrievanceRepository
    {
        //create Method 
        void Add(Grievance grievance);

        //Read method hai 
        List<Grievance> GetAllGrievances();

       
        //Update ke liye mene ek method banaya Interface me
        Grievance GetById(int id);
        void update(Grievance grievance);
        void Delete(int id);
    }
}
