using Blockchain.Interfaces;
using System.Security.Cryptography;


namespace Blockchain
{
    public static class BlockChainExtension
    {
        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream st = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(st))
            {
                bw.Write(block.Data);
                bw.Write(block.Nonce);
                bw.Write(block.TimeStamp.ToBinary());
                bw.Write(block.PrevHash);

                var toArray = st.ToArray();

                return sha.ComputeHash(toArray);
            }
        }

        public static byte[] MineHash(this IBlock block, byte[] difficulty)
        {
            if (difficulty == null) throw new ArgumentNullException(nameof(difficulty));

            byte[] hash = new byte[0];
            int d = difficulty.Length;

            while (!hash.Take(d).SequenceEqual(difficulty))
            {
                block.Nonce++;
                hash = block.GenerateHash();
            }

            return hash;
        }
        public static bool IsValid(this IBlock block)
        {
            var blk = block.GenerateHash();
            return block.Hash.SequenceEqual(blk);
        }

        public static bool IsValid(this IEnumerable<IBlock> items)
        {
            var enumerable = items.ToList();
            return enumerable.Zip(enumerable.Skip(1), Tuple.Create).All(block => block.Item2.IsValid() && block.Item2.IsValidPreviousBlock(block.Item1));
        }        
        public static bool IsValidPreviousBlock(this IBlock block, IBlock previousBlock)
        {
            if (previousBlock == null) throw new ArgumentNullException();

            var previous = previousBlock.GenerateHash();
            return previousBlock.IsValid() && block.PrevHash.SequenceEqual(previous);
        }
    }
}
