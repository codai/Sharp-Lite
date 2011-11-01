using System;

namespace MyStore.Domain
{
    public class Money
    {
        protected Money() { }

        public Money(decimal amount) {
            Amount = amount;
        }

        public decimal Amount { get; set; }

        public override string ToString() {
            return String.Format("{0:c}", Amount);
        }
    }
}
