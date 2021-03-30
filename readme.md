﻿![](https://secouchermoinsbete.fr/images/gfx/header.png?a18f75db)

# SeCoucherMoinsBeteApi


Comme son nom l'indique, c'est une petite api qui permet de récupérer des anecdotes depuis le site https://secouchermoinsbete.fr/.

Cela m'a servit sur des applications faisant de longs traitements, afin de rendre la chose moins ennuyante, j'affiche des anecdotes le temps de l'attente.

## Exemple
![image](https://user-images.githubusercontent.com/17864005/112998245-25b54980-916e-11eb-98bb-3c9dab8d5707.png)


## Installation

```Powershell
Install-Package SeCoucherMoinsBeteApi
```

## Utilisation

```csharp
// Récupération d'anecdotes aléatoires
var anecdotes = await ScmbApi.AnecdotesAleatoires(max: 3);

foreach (var anecdote in anecdotes)
{
    Console.WriteLine($"#{anecdote.Id} : {anecdote.Body}\n");
}
```

Sortie :

```
#54906 : L'Arche russe est un film réalisé par Alexandre Sokourov sorti en 2002. Le film dure 1h36 et a été réalisé en un seul plan-séquence ! Plusieurs mois de répétitions furent nécessaires afin que les techniciens et tous les participants (850 acteurs, un millier de figurants) sachent parfaitement ce qu'ils devaient faire. Il fut tourné en une seule journée au musée de l'Ermitage et la quatrième prise fut la bonne.

#65834 : C'est Louis Armstrong qui a popularisé le scat dans le jazz, cet enchaînement d'onomatopées utilisées à la place des paroles. Il l'aurait utilisé pour la première fois lors de l'enregistrement de son titre "Heebie Jeebies", car il aurait fait tomber ses paroles en faisant le clown et dût donc improviser le reste de la chanson.

#42078 : Suite à un taux de suicide très élevé en Corée du Sud, Samsung Life Insurance a mis en place des rambardes lumineuses sur le pont Mapo, constituées de 2200 lampes et équipées de capteurs, qui diffusent des messages réconfortants ainsi que des photos tout au long des 2,2 kilomètres. Des téléphones sont présents pour joindre un service d'assistance. L'opération est un succès, car le taux de suicide, élevé sur le pont, a baissé de près de 80%.
```
