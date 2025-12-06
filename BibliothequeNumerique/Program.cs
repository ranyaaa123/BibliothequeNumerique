using System;
using System.Globalization;

namespace BibliothequeNumerique
{
    class Program
    {
        static Bibliotheque bibliotheque = new Bibliotheque();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool continuer = true;

            while (continuer)
            {
                AfficherMenu();
                string choix = Console.ReadLine();

                try
                {
                    switch (choix)
                    {
                        case "1":
                            AjouterDocument();
                            break;
                        case "2":
                            bibliotheque.AfficherTous();
                            break;
                        case "3":
                            RechercherDocument();
                            break;
                        case "4":
                            SupprimerDocument();
                            break;
                        case "5":
                            SauvegarderBibliotheque();
                            break;
                        case "6":
                            ChargerBibliotheque();
                            break;
                        case "7":
                            continuer = false;
                            Console.WriteLine("Au revoir!");
                            break;
                        default:
                            Console.WriteLine("Choix invalide. Veuillez réessayer.");
                            break;
                    }
                }
                catch (DocumentNonTrouveException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur: {ex.Message}");
                }

                if (continuer)
                {
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey();
                }
            }
        }

        static void AfficherMenu()
        {
            Console.Clear();
            Console.WriteLine(" +++  BIBLIOTHÈQUE NUMÉRIQUE - MENU  +++");
            Console.WriteLine("1. Ajouter un document");
            Console.WriteLine("2. Afficher tous les documents");
            Console.WriteLine("3. Rechercher par mot-clé");
            Console.WriteLine("4. Supprimer un document");
            Console.WriteLine("5. Sauvegarder dans un fichier");
            Console.WriteLine("6. Charger depuis un fichier");
            Console.WriteLine("7. Quitter");
            Console.WriteLine("----------------------------------------");
            Console.Write("Votre choix: ");
        }

        static void AjouterDocument()
        {
            Console.WriteLine("\n-- AJOUTER UN DOCUMENT --");
            Console.WriteLine("Type de document:");
            Console.WriteLine("1. Livre");
            Console.WriteLine("2. Magazine");
            Console.WriteLine("3. Document PDF");
            Console.Write("Choix: ");
            string typeChoix = Console.ReadLine();

            Console.Write("Titre: ");
            string titre = Console.ReadLine();

            Console.Write("Auteur: ");
            string auteur = Console.ReadLine();

            Console.Write("Année: ");
            int annee = int.Parse(Console.ReadLine());

            switch (typeChoix)
            {
                case "1":
                    Console.Write("Nombre de pages: ");
                    int pages = int.Parse(Console.ReadLine());
                    bibliotheque.AjouterDocument(new Livre(titre, auteur, annee, pages));
                    break;

                case "2":
                    Console.Write("Numéro: ");
                    int numero = int.Parse(Console.ReadLine());
                    bibliotheque.AjouterDocument(new Magazine(titre, auteur, annee, numero));
                    break;

                case "3":
                    Console.Write("Taille en Mo: ");
                    double taille = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    bibliotheque.AjouterDocument(new DocumentPDF(titre, auteur, annee, taille));
                    break;

                default:
                    Console.WriteLine("Type invalide.");
                    break;
            }
        }

        static void RechercherDocument()
        {
            Console.WriteLine("\n-- RECHERCHER UN DOCUMENT --");
            Console.Write("Mot-clé (titre ou auteur): ");
            string motCle = Console.ReadLine();

            var resultats = bibliotheque.Rechercher(motCle);
            
            Console.WriteLine($"\n{resultats.Count} résultat(s) trouvé(s):\n");
            foreach (var doc in resultats)
            {
                doc.AfficherDetails();
            }
        }

        static void SupprimerDocument()
        {
            Console.WriteLine("\n-- SUPPRIMER UN DOCUMENT --");
            Console.Write("ID du document: ");
            string idStr = Console.ReadLine();

            if (Guid.TryParse(idStr, out Guid id))
            {
                bibliotheque.SupprimerDocument(id);
            }
            else
            {
                Console.WriteLine("Format d'ID invalide.");
            }
        }

        static void SauvegarderBibliotheque()
        {
            Console.WriteLine("\n-- SAUVEGARDER LA BIBLIOTHÈQUE --");
            Console.Write("Nom du fichier (ex: bibliotheque.txt): ");
            string fichier = Console.ReadLine();

            bibliotheque.Sauvegarder(fichier);
        }

        static void ChargerBibliotheque()
        {
            Console.WriteLine("\n-- CHARGER LA BIBLIOTHÈQUE --");
            Console.Write("Nom du fichier: ");
            string fichier = Console.ReadLine();

            bibliotheque.Charger(fichier);
        }
    }
}