# SpaghettiTableRace
This C# console application is designed to show race condition and deadlock.
A race condition occurs when two threads access a shared variable at the same time. The first thread reads the variable, and the second thread reads the same value from the variable.
A deadlock occurs when two threads each lock a different variable at the same time and then try to lock the variable that the other thread already locked. As a result, each thread stops executing and waits for the other thread to release the variable.

Here's the senario:

- N people sit at a round table with a bowl of spaghetti in the center.
- Single chopsticks are placed to the right and left of each person.
- Each person can only eat when they have both left and right chopsticks and only once.
- Each chopstick can be held by only one person at a time and only if it is not being used by another person.
- After the person finishes eating they need to release each of the chopsticks so they are available to others.
- A person can grab either the chopstick to the left or the right as they become available but cannot start eating until they have both in their possession.
- There is no limit on the amount of spaghetti.
- The program ends when each of the N people have eaten
