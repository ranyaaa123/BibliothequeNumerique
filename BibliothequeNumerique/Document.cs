using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequeNumerique
{
        public abstract class Document
        {
            public Guid Id { get; set; }
            public string Titre { get; set; }
            public string Auteur { get; set; }
            public int Annee { get; set; }

            public Document(string titre, string auteur, int annee)
            {
                Id = Guid.NewGuid();
                Titre = titre;
                Auteur = auteur;
                Annee = annee;
            }

            public Document(Guid id, string titre, string auteur, int annee)
            {
                Id = id;
                Titre = titre;
                Auteur = auteur;
                Annee = annee;
            }

            public abstract void AfficherDetails();

            public abstract string ToCSV();
        }
    
}
