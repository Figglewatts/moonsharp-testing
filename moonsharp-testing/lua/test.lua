function incrementHandler(o, a)
	print("incremented")
end

StateClass.CounterIncremented.add(incrementHandler)
value = StateClass.incrementCounter(1)
value = StateClass.incrementCounter(3)

print(value)