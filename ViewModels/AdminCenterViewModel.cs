using Die1Er_Projektarbeit.Models;

namespace Die1Er_Projektarbeit.ViewModels
{
    public class AdminCenterViewModel
    {
        public List<Benutzer> BenutzerListe { get; set; }

        public List<Berufsbereich> Berufsbereiche { get; set; }

        public Berufsbereich newBerufsbereich { get; set; }
    }
}
