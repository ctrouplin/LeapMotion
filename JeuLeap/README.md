Ce programme permet de réaliser d'envoyer des votes (les requetes sont recherchées à partir de http://esaip.westeurope.cloudapp.azure.com/api/requests).
Pour ce vote il faut utiliser une LeapMotion.
Le vote OUI se fait avec la main droite.
Le vote NON se fait avec la main gauche.

1) Le programme fait une requete GET sur http://esaip.westeurope.cloudapp.azure.com/api/requests pour optenir les demandes ERASMUS
2) On demande au votant de lever sa main droite ou gauche en fonction de la réponse voulu,
3) La réponse est renvoyée vers l'API avec une requete POST.