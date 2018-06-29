using S.S.L.Web.Models.CustomViewModels;

namespace S.S.L.Web.Models.AdminViewModels
{
    public class DashboardViewModel
    {
        //mentee count
        //mentor count
        //forum count
        //todos
        //thought
        //

        public int Mentees { get; set; }
        public int Facilitatators { get; set; }
        public TodoViewModel TodoModel { get; set; }
    }
}