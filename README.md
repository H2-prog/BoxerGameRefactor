I skal "refactor" boxe spillet i sådan en måde at alle handlingerne skal gøres generiske

 

Spillet er slut efter X Antal runder
Hver runde er 10 slag 
Hver gang der er matematiske udregninger skal det i en seperat funktion
 

f.eks {player1.name} hits {player2.name} for {player1.strength + player1.crit}

kunne godt refactors til 

f.eks {player1.name} hits {player2.name} for (dmg(a, b))

 

public function dmg(int a, int b)

 

a = 2

b = 2

c = a + b

return c