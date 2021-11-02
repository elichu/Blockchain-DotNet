using Blockchain.Interfaces;
using System.Collections;

namespace Blockchain.Classes
{
    public class BlockChain : IEnumerable<IBlock>
    {
        private List<IBlock> items = new List<IBlock>();

        public BlockChain(byte[] difficulty, IBlock genesis)
        {
            Difficulty = difficulty;
            genesis.Hash = genesis.MineHash(difficulty);
            Items.Add(genesis);
        }
        public List<IBlock> Items
        {
            get => items;
            set => items = value;
        }
        public int Count => Items.Count;
        public IBlock this[int Index]
        {
            get => items[Index];
            set => items[Index] = value;
        }

        //globals
        public byte[] Difficulty { get; }
        public void Add(IBlock item)
        {
            if (Items.LastOrDefault() != null)
            {
                item.PrevHash = Items.LastOrDefault()?.Hash;
            }

            item.Hash = item.MineHash(Difficulty);
            Items.Add(item);
        }

        public IEnumerator<IBlock> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
