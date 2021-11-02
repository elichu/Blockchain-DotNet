using Blockchain;
using Blockchain.Classes;
using Blockchain.Interfaces;


class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random(DateTime.UtcNow.Millisecond);

        //initial block
        IBlock genesis = new Block(new byte[] {0x00, 0x00, 0x00, 0x00, 0x00 });
        byte[] difficulty = new byte[] { 0x00, 0x00 };

        BlockChain chain = new BlockChain(difficulty, genesis);

        //arbitary # of blocks to make, set at 20
        for(int i = 0; i < 20; i++)
        {
            var data = Enumerable.Range(0, 256).Select(p => (byte)rand.Next());
            chain.Add(new Block(data.ToArray()));
            Console.WriteLine(chain.LastOrDefault()?.ToString());

            Console.WriteLine($"Chain Is Valid: {chain.IsValid()}");
        }

        Console.ReadLine();

    }

}