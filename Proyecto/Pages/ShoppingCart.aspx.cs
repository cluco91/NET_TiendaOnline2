using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Models;

public partial class Pages_ShoppingCart : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userId = User.Identity.GetUserId();

        GetPurchasesInCart(userId);        
    }

    protected void Delete_Item(object sender, EventArgs e)
    {
        LinkButton selectedLink = (LinkButton)sender;
        string link = selectedLink.ID.Replace("del", "");
        int cartId = Convert.ToInt32(link);

        var cartModel = new CartModel();
        cartModel.DeleteCart(cartId);

        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }

    private void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList selectedList = (DropDownList)sender;
        int cartId = Convert.ToInt32(selectedList.ID);
        int quantity = Convert.ToInt32(selectedList.SelectedValue);

        CartModel cartModel = new CartModel();
        cartModel.UpdateQuantity(cartId, quantity);
        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }

    private void GetPurchasesInCart(string userId)
    {
        CartModel cartModel = new CartModel();
        double subTotal = 0;

        List<Cart> purchaseList = cartModel.GetOrdersInCart(userId);        
        CreateShopTable(purchaseList, out subTotal);

        double vat = subTotal * 0.21;
        double totalAmount = subTotal + 15 + vat;

        litTotal.Text = "$ " + subTotal;
        litVat.Text = "$ " + vat;
        litTotalAmount.Text = "$ " + totalAmount;

    }

    private void CreateShopTable(IEnumerable<Cart> carts, out double subTotal)
    {
        subTotal = new double();
        ProductModel model = new ProductModel();

        foreach (Cart cart in carts)
        {
            Product product = model.GetProduct(cart.ProductID);

            ImageButton btnImage = new ImageButton
            {
                ImageUrl = string.Format("~/Images/Products/{0}", product.Image),
                PostBackUrl = string.Format("~/Pages/Product.aspx?id={0}", product.ID)
            };

            LinkButton lnkDelete = new LinkButton
            {
                PostBackUrl = string.Format("~/Pages/ShoppingCart.aspx?productId={0}", cart.ID),
                Text = "Borrar Item",
                ID = "del" + cart.ID,
            };

            lnkDelete.Click += Delete_Item;

            int[] amount = Enumerable.Range(1, 20).ToArray();
            DropDownList ddlAmount = new DropDownList
            {
                DataSource = amount,
                AppendDataBoundItems = true,
                AutoPostBack = true,
                ID = cart.ID.ToString()
            };
            ddlAmount.DataBind();
            ddlAmount.SelectedValue = cart.Amount.ToString();
            ddlAmount.SelectedIndexChanged += ddlAmount_SelectedIndexChanged;

            Table table = new Table { CssClass = "CartTable" };
            TableRow row1 = new TableRow();
            TableRow row2 = new TableRow();

            TableCell cell1_1 = new TableCell { RowSpan = 2, Width = 50 };
            TableCell cell1_2 = new TableCell { Text = string.Format("<h4>{0}</h4><br />{1}<br/>En Stock",
                product.Name, "Numero Item:" + product.ID), HorizontalAlign = HorizontalAlign.Left, Width = 350, };
            TableCell cell1_3 = new TableCell { Text = "Precio Unitario<hr/>" };
            TableCell cell1_4 = new TableCell { Text = "Cantidad<hr/>" };
            TableCell cell1_5 = new TableCell { Text = "Total Item <hr/>" };
            TableCell cell1_6 = new TableCell();

            TableCell cell2_1 = new TableCell();
            TableCell cell2_2 = new TableCell { Text = "$ " + product.Price };
            TableCell cell2_3 = new TableCell();
            TableCell cell2_4 = new TableCell { Text = "$ " + Math.Round((cart.Amount * product.Price), 2) };
            TableCell cell2_5 = new TableCell();


            cell1_1.Controls.Add(btnImage);
            cell1_6.Controls.Add(lnkDelete);
            cell2_3.Controls.Add(ddlAmount);

            row1.Cells.Add(cell1_1);
            row1.Cells.Add(cell1_2);
            row1.Cells.Add(cell1_3);
            row1.Cells.Add(cell1_4);
            row1.Cells.Add(cell1_5);
            row1.Cells.Add(cell1_6);

            row2.Cells.Add(cell2_1);
            row2.Cells.Add(cell2_2);
            row2.Cells.Add(cell2_3);
            row2.Cells.Add(cell2_4);
            row2.Cells.Add(cell2_5);
            table.Rows.Add(row1);
            table.Rows.Add(row2);
            pnlShoppingCart.Controls.Add(table);

            subTotal += (cart.Amount * product.Price);
        }

        Session[User.Identity.GetUserId()] = carts;
    }

    private string GeneratePaypalButton(double subTotal)
    {

        string paypal = string.Format(
            @"<script async='async' src='https://gist.github.com/cluco91/235ba5368c7921d15fcf9bb82b13b14e.js' 
                data-button='buynow' 
                data-name='Compra Paypal' 
                data-quantity=1
                data-amount='{0}' 
                data-tax='{1}'
                data-shipping='15'
                data-callback='http://localhost:50992/Pages/Success.aspx'
                data-sendback='http://localhost:50992/Pages/Success.aspx'
                data-env='sandbox'>
             </script>", subTotal, (subTotal * 0.21));

        return paypal;
    }

    
}