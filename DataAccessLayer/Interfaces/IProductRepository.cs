﻿using DataAccessLayer.Models;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
}
