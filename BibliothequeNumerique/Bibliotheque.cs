using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace BibliothequeNumerique
{
        public class Bibliotheque
        {
            private List<Document> documents;

            public Bibliotheque()
            {
                documents = new List<Document>();
            }

            public void AjouterDocument(Document document)
            {
                documents.Add(document);
                Console.WriteLine("Document ajouté avec succès!");
            }

            public void SupprimerDocument(Guid id)
            {
                Document doc = documents.FirstOrDefault(d => d.Id == id);

                if (doc == null)
                {
                    throw new DocumentNonTrouveException($"Le document avec l'ID {id} n'existe pas.");
                }

                documents.Remove(doc);
                Console.WriteLine("Document supprimé avec succès!");
            }

            public List<Document> Rechercher(string motCle)
            {
                if (string.IsNullOrWhiteSpace(motCle))
                {
                    throw new ArgumentException("Le mot-clé ne peut pas être vide.");
                }

                var resultats = documents.Where(d =>
                    d.Titre.Contains(motCle, StringComparison.OrdinalIgnoreCase) ||
                    d.Auteur.Contains(motCle, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                if (resultats.Count == 0)
                {
                    throw new DocumentNonTrouveException($"Aucun document trouvé avec le mot-clé '{motCle}'.");
                }

                return resultats;
            }

            public void AfficherTous()
            {
                if (documents.Count == 0)
                {
                    Console.WriteLine("La bibliothèque est vide.");
                    return;
                }

                Console.WriteLine($"\n-- BIBLIOTHÈQUE ({documents.Count} document(s)) --\n");
                foreach (var doc in documents)
                {
                    doc.AfficherDetails();
                }
            }

            public void Sauvegarder(string cheminFichier)
            {
                try
                {
                    using (FileStream fs = new FileStream(cheminFichier, FileMode.Create))
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        foreach (var doc in documents)
                        {
                            writer.WriteLine(doc.ToCSV());
                        }
                    }
                    Console.WriteLine($"Bibliothèque sauvegardée dans {cheminFichier}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Erreur d'accès au fichier: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la sauvegarde: {ex.Message}");
                    throw;
                }
            }

            public void Charger(string cheminFichier)
            {
                if (!File.Exists(cheminFichier))
                {
                    throw new FileNotFoundException($"Le fichier '{cheminFichier}' est introuvable.");
                }

                try
                {
                    using (FileStream fs = new FileStream(cheminFichier, FileMode.Open))
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        documents.Clear();
                        string ligne;

                        while ((ligne = reader.ReadLine()) != null)
                        {
                            try
                            {
                                Document doc = ParseLigneCSV(ligne);
                                documents.Add(doc);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ligne ignorée (format incorrect): {ligne}");
                                Console.WriteLine($"Erreur: {ex.Message}");
                            }
                        }
                    }
                    Console.WriteLine($"{documents.Count} document(s) chargé(s) depuis {cheminFichier}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Erreur d'accès au fichier: {ex.Message}");
                    throw;
                }
            }

            private Document ParseLigneCSV(string ligne)
            {
                string[] parties = ligne.Split(';');

                if (parties.Length < 6)
                {
                    throw new FormatException("Format de ligne incorrect: nombre de colonnes insuffisant.");
                }

                string type = parties[0];
                Guid id = Guid.Parse(parties[1]);
                string titre = parties[2];
                string auteur = parties[3];
                int annee = int.Parse(parties[4]);

                switch (type)
                {
                    case "Livre":
                        int nombrePages = int.Parse(parties[5]);
                        return new Livre(id, titre, auteur, annee, nombrePages);

                    case "Magazine":
                        int numero = int.Parse(parties[5]);
                        return new Magazine(id, titre, auteur, annee, numero);

                    case "DocumentPDF":
                        double taille = double.Parse(parties[5], CultureInfo.InvariantCulture);
                        return new DocumentPDF(id, titre, auteur, annee, taille);

                    default:
                        throw new FormatException($"Type de document inconnu: {type}");
                }
            }
        }
    
}
