using System;
namespace DxHumanModel.MolecularAutomata
{
    public interface IMolecule
    {
        decimal AtomicMass { get; set; }
        decimal Density { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        int Charge { get; set; }
    }
    public interface IMoleculeIdentifier
    {
        string Id { get; set; }
    }
 
    // Basically just mmol of Substances in any volume - should just equate to a concentration
    
   
}
