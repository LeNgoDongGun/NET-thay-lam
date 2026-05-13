class Repository<T>
{
private List<T> list = new List<T>();
public void Add(T item)
{
list.Add(item);
}
public List<T> GetAll()
{
return list;
}
}