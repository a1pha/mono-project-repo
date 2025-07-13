# Pointer Wars 2025: Linked List Edition Level 1

## This Level's Tasks
1. Review linked_list.h and implement those functions in linked_list.c
2. Ensure that your code compiles with no warnings nor errors.
3. Pass functional tests by running the function test suite. You can 
   do this by invoking 'make run_functional_tests'.
4. Pass valgrind checks to ensure your code does not leak memory. You
   can do this by invoking 'make run_valgrind_tests'

## More Information For You
There are three C structures in linked_list.h: linked_list, node, and iterator. 
The linked_list is composed of nodes, and the iterator structure is 
used by a few functions, namely linked_list_iterate(). 

In terms of memory allocation and deallocation, your library depends 
on user-specified malloc() and free() functions, registered 
via the linked_list_register_malloc() and linked_list_register_free() 
functions. See the malloc_fptr and free_fptr function pointer variables 
in linked_list.c, which you can use in your implementation of your linked 
list. One of the reasons for this is code like instrumented_malloc() 
in linked_list_test_program.c. Another reason is to allow you 
to specify to me whether you'd like to use a memory allocation 
library other than libc's implementation of malloc()/free().

# Paid Participants: What To Do Once Finished
Send us email (pointerwars2025@gmail.com) with a link to your GitHub page,
or attach files to the email. If your code passes functional tests and
valgrind tests, we'll send you code feedback and performance numbers.
Level 1 performance numbers are microbenchmarks that measure down to the
nanonsecond how fast your linked list operations are.

We'll send you Level 2 instructions as well.

# Free Participants: What To Do Once Finished
Send us email (pointerwars2025@gmail.com) and we will send along level 2
instructions for you. If you have changed your mind and want code and 
performance numbers, you are more than welcome to send mail and we will
send you an invoice to upgrade to the paid tier.

Some general advice on things to watch out for in Level 2 will be provided
to you as well.

# Looking Ahead to Level 2
Level 2 will provide you with software you can run locally to test how
fast your code runs. With a few tweaks and best practices, you can use
this software to determine what a change does in terms of performance.

Paid participants have their runs performed on our machines, so they
can be compared against others, and also receive PMU counter feedback,
along with interpretation of those PMU counters, in level 2. 
