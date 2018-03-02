Ce programme permet de réaliser d'envoyer des votes (les requetes sont recherchées à partir de http://esaip.westeurope.cloudapp.azure.com/api/requests).
Pour ce vote il faut utiliser une LeapMotion.
Le vote OUI se fait avec la main droite.
Le vote NON se fait avec la main gauche.
Pour qu'un vote soit prit en compte il faut que le taux de certitude soit > ou = à 0.8.
1) Le programme fait une requete GET sur http://esaip.westeurope.cloudapp.azure.com/api/requests pour optenir les demandes ERASMUS
2) On demande au votant de lever sa main droite ou gauche en fonction de la réponse voulu,
3) La réponse est renvoyée vers l'API avec une requete POST.

Dans cette partie du projet, et d'après ce qui a été fait dans la partie API,
Il faut pouvoir récupérer le login et mot de passe du votant, le mettre dans une chaine de type login:motdepasse,
coder ce login:motdepasse en base 64 et rajouter devant "Basic ". On optient alors une chaine de type Basic bG9naW46cGFzc3dvcmQ=
que l'on pourra interpreter du côté de l'API