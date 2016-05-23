package euler.helpers;

import java.util.ArrayList;
import java.util.Iterator;

public class LimitedCollection<T> implements Iterable<T> {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1204206811413110582L;
	private ArrayList<T> collection;
	private T filler;
	private int size;
	
	public LimitedCollection(int size, T filler) {
		this.size = size;
		this.filler = filler;
		collection = new ArrayList<T>();
		for (int i = 0; i < size; i++)
			add(filler);
	}
	
	public int size() {
		return collection.size();
	}
	
	public boolean isEmpty() {
		if (size == 0)
			return true;
		for (T t : collection)
			if (t.equals(filler))
				return false;
		return true;
	}
	
	public boolean contains(T t) {
		return collection.indexOf(t) >= 0;
	}
	
	public int indexOf(T t) {
		return collection.indexOf(t);
	}
	
	public int lastIndexOf(T t) {
		return collection.lastIndexOf(t);
	}
	
	public T get(int index) {
		return collection.get(index);
	}
	
	public T set(int index, T element) {
		return collection.set(index, element);
	}
	
	public boolean add(T t) {
		boolean out = collection.add(t);
		if (out)
			collection.remove(0);
		return out;
	}
	
	public void add(int index, T element) {
		collection.remove(0);
		collection.add(index, element);
	}
	
	public T remove(int index) {
		collection.add(0, filler);
		return collection.remove(index);
	}
	
	public boolean remove(Object o) {
		boolean out = collection.remove(o);
		if (out)
			collection.add(0, filler);
		return out;
	}
	
	public void clear() {
		collection.clear();
	}
	
	public void setSize(int size) {
		if (size < this.size)
			for (int i = 0; i < this.size - size; i++)
				collection.remove(size);
		else if (size > this.size)
			for (int i = 0; i < size - this.size; i++)
				collection.add(filler);
		this.size = size;
	}
	
	public void setFiller(T filler) {
		this.filler = filler;
	}
	
	@Override
	public Iterator<T> iterator() {
		return collection.iterator();
	}
}
