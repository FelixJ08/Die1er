using Die1Er_Projektarbeit.Models;

namespace Die1Er_Projektarbeit.ViewModels
{
    public class ThemaErstellenViewModel
    {
        public List<Berufsbereich> Berufsbereiche { get; set; }

        public Thema newThema { get; set; }

        public int SelectedBerufsbereichId { get; set; }  
    }
}
