Projet en C# pour gérer une bibliothèque numérique. Permet d'ajouter, rechercher, supprimer et sauvegarder des documents (livres, magazines, fichiers PDF).

##  Fonctionnalités

- Ajouter des documents (Livre, Magazine, PDF)
- Afficher tous les documents
- Rechercher par mot-clé (titre ou auteur)
- Supprimer un document
- Sauvegarder dans un fichier CSV
- Charger depuis un fichier CSV

### Classes principales

- **`Document`** : Classe abstraite de base avec les propriétés communes (Id, Titre, Auteur, Année)
- **`Livre`** : Hérite de Document, ajoute le nombre de pages
- **`Magazine`** : Hérite de Document, ajoute le numéro
- **`DocumentPDF`** : Hérite de Document, ajoute la taille en Mo
- **`Bibliotheque`** : Gère la collection de documents et les opérations CRUD
- **`DocumentNonTrouveException`** : Exception personnalisée pour la gestion d'erreurs

##  Utilisation

Au lancement, un menu s'affiche avec les options suivantes :
```
1. Ajouter un document
2. Afficher tous les documents
3. Rechercher par mot-clé
4. Supprimer un document
5. Sauvegarder dans un fichier
6. Charger depuis un fichier
7. Quitter
```

##  Structure du projet
```
BibliothequeNumerique/
├── Document.cs                      # Classe abstraite de base
├── Livre.cs                         # Classe Livre
├── Magazine.cs                      # Classe Magazine
├── DocumentPDF.cs                   # Classe DocumentPDF
├── DocumentNonTrouveException.cs    # Exception personnalisée
├── Bibliotheque.cs                  # Gestion de la bibliothèque
└── Program.cs                       # Menu et point d'entrée
```


