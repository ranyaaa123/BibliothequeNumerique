using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequeNumerique
{
    public class DocumentPDF : Document
        {
            public double TailleEnMo { get; set; }

            public DocumentPDF(string titre, string auteur, int annee, double tailleEnMo)
                : base(titre, auteur, annee)
            {
                TailleEnMo = tailleEnMo;
            }

            public DocumentPDF(Guid id, string titre, string auteur, int annee, double tailleEnMo)
                : base(id, titre, auteur, annee)
            {
                TailleEnMo = tailleEnMo;
            }

            public override void AfficherDetails()
            {
                Console.WriteLine($"-- DOCUMENT PDF --");
                Console.WriteLine($"ID: {Id}");
                Console.WriteLine($"Titre: {Titre}");
                Console.WriteLine($"Auteur: {Auteur}");
                Console.WriteLine($"Année: {Annee}");
                Console.WriteLine($"Taille: {TailleEnMo} Mo");
                Console.WriteLine();
            }

            public override string ToCSV()
            {
                return $"DocumentPDF;{Id};{Titre};{Auteur};{Annee};{TailleEnMo}";
            }
        }
    
}
