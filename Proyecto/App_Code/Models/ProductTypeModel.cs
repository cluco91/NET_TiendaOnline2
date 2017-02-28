using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// </summary>

public class ProductTypeModel
{
    public string InsertProductType(ProductType productType)
    {
        try
        {
            GarageEntities db = new GarageEntities();
            db.ProductTypes.Add(productType);
            db.SaveChanges();

            return productType.Name + "fue agregado con exito";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string UpdateProductType(int id, ProductType productType)
    {
        try
        {
            GarageEntities db = new GarageEntities();

            ProductType p = db.ProductTypes.Find(id);

            p.Name = productType.Name;

            db.SaveChanges();
            return productType.Name + "fue actualizado con exito";

        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string DeleteProductType(int id)
    {
        try
        {
            GarageEntities db = new GarageEntities();
            ProductType productType = db.ProductTypes.Find(id);

            db.ProductTypes.Attach(productType);
            db.ProductTypes.Remove(productType);
            db.SaveChanges();

            return productType.Name + "fue borrado con exito";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }
}