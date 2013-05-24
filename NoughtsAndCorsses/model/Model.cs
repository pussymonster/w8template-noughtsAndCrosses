using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCorsses.model
{
    class Model
    {
        public enum Player { CROSS, NOUGHT, EMPTY = 50 };
        
        private static EndCondition TIE = new EndCondition(true, Player.EMPTY, 0, 0, 0);
        private static EndCondition NOT_YET = new EndCondition(false, Player.EMPTY, 0, 0, 0);
        private int size;

        private Player currentPlayer;
        private List<Player> model;
        private bool locked;

        public Player CurrentPlayer { get { return currentPlayer; } }
        public int emptyFieldCounter;

        public Model(int size)
        {
            model = new List<Player>(size);
            for (int i = 0; i < size; i++)
                model.Add(Player.EMPTY);
            currentPlayer = Player.NOUGHT;
            emptyFieldCounter = size;
            locked = false;
            this.size = size;
        }

        public Boolean isSet(int index)
        {
            return !locked && model[index] == Player.EMPTY;
        }

        public void setPlayer(int index)
        {
            emptyFieldCounter--;
            model[index] = currentPlayer;
        }

        public void nextPlayer()
        {
            currentPlayer = currentPlayer.Equals(Player.NOUGHT) ? Player.CROSS : Player.NOUGHT;
        }

        public void lockModel()
        {
            locked = true;
        }

        public EndCondition checkEnd()
        {
            int result = 3 * (int)(currentPlayer.Equals(Player.NOUGHT)? Player.NOUGHT: Player.CROSS);

            for (int i = 0; i < size; i += 3)
            {
                int sum = ((int)model[i] + (int)model[i + 1] + (int)model[i + 2]);
                if (((int)model[i] + (int)model[i + 1] + (int)model[i + 2]) == result)
                    return new EndCondition(true, currentPlayer, i, i + 1, i +2);
            }

            for (int i = 0; i < Math.Sqrt(size); i++)
            {
                if (((int)model[i] + (int)model[i + 3] + (int)model[i + 6]) == result)
                    return new EndCondition(true, currentPlayer, i, i + 3, i + 6);
            }

            if (((int)model[0] + (int)model[4] + (int)model[8]) == result)
                return new EndCondition(true, currentPlayer, 0, 4, 8);

            if (((int)model[2] + (int)model[4] + (int)model[6]) == result)
                return new EndCondition(true, currentPlayer, 2, 4, 6);

            return emptyFieldCounter == 0? TIE : NOT_YET;
        }
    }
}
