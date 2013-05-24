using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCorsses.model
{
    class EndCondition
    {
        private int first;
        private int second;
        private int third;
        private Model.Player winner;
        private bool end;

        public EndCondition(bool end, Model.Player winner, int first, int second, int third)
        {
            this.end = end;
            this.winner = winner;
            this.first = first;
            this.second = second;
            this.third = third;
        }

        public int First { get { return first; } }
        public int Second { get { return second; } }
        public int Third { get { return third; } }
        public Model.Player Winner { get { return winner; } }
        public bool End { get { return end; } }

    }
}
