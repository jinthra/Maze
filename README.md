# Maze

Maze est un puzzle game dans lequel le joueur incarne un petit personnage qui doit trouver un moyen de sortir d'un labyrinthe. Pour cela, le joueur doit redoubler d'ingéniosité en déplaçant des caisses aux bons endroits pour ouvrir des passages et ainsi pouvoir sortir.

Ce projet est réalisé dans le cadre du cours C# de la He-Arc

## Exécuter le jeu
Pour lancer l'application, il faut tout d'abord télécharger le projet et l'ouvrir dans Visual Studio pour pouvoir le build. L'executable devrait alors se trouver dans un dossier *bin/Debug* ou *bin/Release* suivant le mode de build que vous avez sélectionné. Ensuite, il faut ajouter les niveaux de base qui sont dans le dossier *mazes*. Pour celà, vous pouvez soit copier le dossier *mazes* à la racine où se trouve l'executable, soit lancer le jeu et utiliser le bonton *Importer un niveau* et les ajouter un par un.

Si vous souhaitez, vous pouvez aussi ajouter un niveau que vous avez créé. Il suffit de faire un fichier texte qui suit la structure des niveaux fournis et les importer.

## Controls en jeu
- Touches directionnelles: déplacer le personnage
- F11: mode fullscreen
- Esc: ouvrir le menu

## Code architecture
La partie logique du jeu se situe dans le dossier *models*. La classe Game s'occupe de créer le jeu et de son comportement. C'est elle qui recoit les inputs et l'ouput. La classe *Level* quant à elle, s'occupe de parser un fichier pour charger un niveau et stock différents blocks créés.

Pour ce qui est des blocks, leur code se situe dans le dossier *block*. Chacun de ces blocks hérite de la classe *Block*. Ainsi, si vous souhaitez en créer un nouveau, il faudra qu'il hérite de la *Block*.

Toutes les images du jeu sont dans un dossier ressources *res*. Pour en ajouter une nouvelle, il faut l'ajouter à ce dossier et changer la proprieté *Build Action* en *Resource*. Vous pouvez aussi relancer le build du jeu et le fichier *Maze.csproj* s'occupera de changer la proprieté *Build Action* en *Resource*.

MainWindow.xaml gère la fenêtre de l'application ainsi que la vue du menu principal. 
GamePage.xaml s'occupe de la vue du jeu et implémente l'interface IInput et IOuput. C'est dans cette classe que le jeu est créé.
