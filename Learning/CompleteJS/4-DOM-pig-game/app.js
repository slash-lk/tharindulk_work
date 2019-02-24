/*
GAME RULES:

- The game has 2 players, playing in rounds
- In each turn, a player rolls a dice as many times as he whishes. Each result get added to his ROUND score
- BUT, if the player rolls a 1, all his ROUND score gets lost. After that, it's the next player's turn
- The player can choose to 'Hold', which means that his ROUND score gets added to his GLBAL score. After that, it's the next player's turn
- The first player to reach 100 points on GLOBAL score wins the game

*/


var scores, roundScore, activePlayer, dice1DOM, dice2DOM, isPlaying, lastDice, winScore;


dice1DOM = document.querySelector('#dice-1');
dice2DOM = document.querySelector('#dice-2');

init();

document.querySelector('.btn-roll').addEventListener('click', () => {
    
    if (!isPlaying) {
        return;
    }

    if (!document.querySelector('.final-score').getAttribute('readonly')) {
        document.querySelector('.final-score').setAttribute('readonly', true);

        if (document.getElementById('win-score').value) {
            winScore = document.getElementById('win-score').value;
        }
        else {
            document.getElementById('win-score').value = winScore;
        }
    }
    

    // 1. Random number
    var dice1 = Math.ceil(Math.random() * 6);
    var dice2 = Math.ceil(Math.random() * 6);

    // 2. Display the result
    dice1DOM.style.display = 'block';
    dice2DOM.style.display = 'block';

    dice1DOM.src = `dice-${dice1}.png`;
    dice2DOM.src = `dice-${dice2}.png`;

    // 3. Update the roundScore if the rolled number was not a 1
    /*if (lastDice == 6 && dice === 6) {
        scores[activePlayer] = 0;
        document.getElementById(`score-${activePlayer}`).textContent = "0";
        document.getElementById(`current-${activePlayer}`).textContent = "0";
        switchPlayer();
    }
    else if (dice !== 1) {
        roundScore += dice;
        document.getElementById(`current-${activePlayer}`).textContent = roundScore;
        lastDice = dice; 
    }
    else {
        switchPlayer();
    }*/

    if (dice1 !== 0 && dice2 !== 0) {
        roundScore += dice1 + dice2;
        document.getElementById(`current-${activePlayer}`).textContent = roundScore;
        //lastDice = dice;
    }
    else {
        scores[activePlayer] = 0;
        document.getElementById(`score-${activePlayer}`).textContent = "0";
        document.getElementById(`current-${activePlayer}`).textContent = "0";
        switchPlayer();
    }
   
});


document.querySelector('.btn-hold').addEventListener('click', () => {

    if (!isPlaying) {
        return;
    }

    scores[activePlayer] += roundScore;
    document.getElementById(`score-${activePlayer}`).textContent = scores[activePlayer];

    // Check if won
    if (scores[activePlayer] >= winScore) {
        document.querySelector(`#name-${activePlayer}`).textContent = "Winner!";
        document.querySelector(`.player-${activePlayer}-panel`).classList.remove('active');
        document.querySelector(`.player-${activePlayer}-panel`).classList.add('winner');

        isPlaying = false;
    } 
    else {
        switchPlayer();  
    }      
});


function switchPlayer() {
    roundScore = 0;
    lastDice = 0;
    document.getElementById(`current-${activePlayer}`).textContent = roundScore;

    activePlayer = (activePlayer === 0) ? 1 : 0;

    document.querySelector('.player-0-panel').classList.toggle('active');
    document.querySelector('.player-1-panel').classList.toggle('active');
    dice1DOM.style.display = 'none';
    dice2DOM.style.display = 'none';
}


function init() {
    scores = [0, 0];
    roundScore = 0;
    activePlayer = 0;
    lastDice = 0;
    isPlaying = true;
    winScore = 100;

    document.getElementById('score-0').textContent = "0";
    document.getElementById('score-1').textContent = "0";
    document.getElementById('current-0').textContent = "0";
    document.getElementById('current-1').textContent = "0";

    document.getElementById('name-0').textContent = "Player 1";
    document.getElementById('name-1').textContent = "Player 2";

    document.querySelector('.player-0-panel').classList.remove('active');
    document.querySelector('.player-0-panel').classList.remove('winner');
    document.querySelector('.player-1-panel').classList.remove('active');
    document.querySelector('.player-1-panel').classList.remove('winner');

    document.querySelector('.player-0-panel').classList.add('active');

    dice1DOM.style.display = 'none';
    dice2DOM.style.display = 'none';

    document.querySelector('.final-score').value = winScore;
    document.querySelector('.final-score').setAttribute('readonly', false);
}


document.querySelector('.btn-new').addEventListener('click', init);



