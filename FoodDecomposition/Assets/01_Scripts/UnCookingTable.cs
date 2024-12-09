using UnityEngine;

public class UnCookingTable : CreatableTable
{
    
    
    public override void StartAnimation()
    {
        
    }

    protected override void HandleEntered(Collider obj)
    {

        IPutable putable = obj.GetComponentInParent<IPutable>();
        Puted(putable);
        
        return;
    }

    protected override void Puted(IPutable putable)
    {
        var list = putable.GetTakableObjects();
        foreach (var obj in list)
        {
            if ((int)obj.Type < Const.RecipeIdx)
            {
                if (obj is Product)
                {
                    Product ingredient = obj as Product;
                    Cooking(ingredient);
                }
            }
        }
    }

    public void Cooking(Product _ingredient)
    {
        
    }
}
