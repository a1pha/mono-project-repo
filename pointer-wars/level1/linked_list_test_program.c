#include <signal.h>
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#include "linked_list.h"

#define TEST(x) printf("Running test " #x "\n"); fflush(stdout);
#define SUBTEST(x) printf("    Executing subtest " #x "\n"); fflush(stdout); \
                   alarm(1);
#define FAIL(cond, msg) if (cond) {\
                        printf("    FAIL! "); \
                        printf(#msg "\n"); \
                        exit(-1);\
                        }
#define PASS(x) printf("PASS!\n"); alarm(0);

bool instrumented_malloc_fail_next             = false;
bool instrumented_malloc_last_alloc_successful = false;

void gracefully_exit_on_suspected_infinite_loop(int signal_number) {
    // Use write() to tell the tester that they're probably stuck
    // in an infinite loop.
    //
    // Why not printf()/fprintf()? It goes against POSIX rules for
    // signal handlers to call a non-reentrant function, of which both
    // of those are. I had no about that constraint prior to writing
    // this function. Cool!
    //
    const char* err_msg = "        Likely stuck in infinite loop! Exiting.\n";
    ssize_t retval      = write(STDOUT_FILENO, err_msg, strlen(err_msg));
    fflush(stdout);

    // We really don't care about whether write() succeeded or failed
    // or whether a partial write occurred. Further, we only install
    // this function to one signal handler, so we can ignore that as well.
    //
    (void)retval;
    (void)signal_number;

    // Exit.
    //
    exit(1);
}

void * instrumented_malloc(size_t size) {
    if (instrumented_malloc_fail_next) {
        instrumented_malloc_fail_next             = false;
	instrumented_malloc_last_alloc_successful = false;
	return NULL;
    }

    void * ptr = malloc(size);
    instrumented_malloc_last_alloc_successful = (ptr != NULL);

    return ptr;
}

void check_null_handling(void) {
    TEST(check_null_handling)

    SUBTEST(linked_list_delete)
    bool status = linked_list_delete(NULL);
    FAIL(status != false,
         "linked_list_delete(NULL) did not return false")

    SUBTEST(linked_list_insert_front)
    status = linked_list_insert_front(NULL, 0);
    FAIL(status != false,
         "linked_list_insert_front(NULL, 0) did not return false")

    SUBTEST(linked_list_insert_end)
    status = linked_list_insert_end(NULL, 0);
    FAIL(status != false,
         "linked_list_insert_end(NULL, 0) did not return false")

    SUBTEST(linked_list_insert)
    status = linked_list_insert(NULL, 0, 0);
    FAIL(status != false,
         "linked_list_insert(NULL, 0, 0) did not return false")

    SUBTEST(linked_list_create_iterator)
    struct iterator * iter = linked_list_create_iterator(NULL, 0);
    FAIL(iter != NULL,
         "linked_list_create_iterator(NULL, 0) did not return NULL")

    SUBTEST(linked_list_delete_iterator)
    status = linked_list_delete_iterator(NULL);
    FAIL(status != false,
         "linked_list_delete_iterator(NULL) did not return false")

    SUBTEST(linked_list_iterate)
    status = linked_list_iterate(NULL);
    FAIL(status != false,
         "linked_list_iterate(NULL) did not return false")

    SUBTEST(linked_list_find)
    size_t index = linked_list_find(NULL, 0);
    FAIL(index != SIZE_MAX,
         "linked_list_find(NULL, 0) did not return SIZE_MAX");

    PASS(check_null_handling)
}

void check_empty_list_properties(void) {
    TEST(check_empty_list_properties)
    SUBTEST(linked_list_create)
    struct linked_list * ll = linked_list_create();

    // Sanity check that linked_list_create() works on memory allocation
    // success.
    //
    FAIL((instrumented_malloc_last_alloc_successful && (ll == NULL)), 
         "linked_list_create() failed when malloc() returned a valid pointer")

    // Check invariant that head is null when empty.
    //
    FAIL((ll->head != NULL),
         "ll->head is non-null in empty linked_list");

    linked_list_delete(ll);

    // Force the memory allocator fail, ensure that NULL is returned.
    //
    SUBTEST(linked_list_memory_alloc_fail)
    instrumented_malloc_fail_next = true;
    ll = linked_list_create();
    FAIL(ll != NULL,
         "linked_list_create() returns non-null pointer on allocation failure")

    // Attempt to create an iterator for index 0.
    //
    SUBTEST(empty_linked_list_iterator)
    ll = linked_list_create();
    struct iterator * iter = linked_list_create_iterator(ll, 0);
    FAIL (iter != NULL,
          "linked_list_create_iterator returned an iterator for an empty linked_list")

    // Cleanup.
    //
    linked_list_delete_iterator(iter);
    linked_list_delete(ll);
    PASS(check_empty_list_properties)
}

void check_insertion_functionality(void) {
    TEST(check_insertion_functionality)
    SUBTEST(check_insert_end)
    // Check insertion at end with an iterator.
    // Inserts 1, 2, 3, 4 into the list, verifies
    // data.
    //
    struct linked_list * ll = linked_list_create();
    size_t ll_size          = SIZE_MAX;
    FAIL(ll == NULL,
         "Failed to create new linked_list (#1)")
    for (size_t i = 1; i <= 4; i++) {
        bool status = linked_list_insert_end(ll, i);
	FAIL(status == false,
             "Failed to insert node into linked_list #1")
    }

    struct iterator * iter = linked_list_create_iterator(ll, 0);
    FAIL(iter == NULL,
         "Failed to create new iterator for linked_list (#1)")

    SUBTEST(iterate_over_linked_list_1)
    for (size_t i = 1; i <= 4; i++) {
        FAIL(iter->data != i,
             "Iterator does not contain correct data for linked_list (#1)")
        FAIL(iter->current_index != (i - 1),
             "Iterator does not contain correct index for linked_list (#1)")

        // Next element.
	//
	linked_list_iterate(iter);
    }
    linked_list_delete(ll);
    linked_list_delete_iterator(iter);

    // Check insertion at front with an iterator.
    // Inserts 4, 3, 2, 1 into the list, verifies data.
    //
    SUBTEST(check_insert_front)
    ll      = linked_list_create();
    ll_size = linked_list_size(ll);
    FAIL(ll_size != 0,
         "linked_list (#2) size is non-zero when created") 
    FAIL(ll == NULL,
         "Failed to create new linked_list (#2)")
    for (size_t i = 4; i != 0; i--) {
        bool status = linked_list_insert_front(ll, i);
	FAIL(status == false,
             "Failed to insert node into linked_list #2")
    }
    ll_size = linked_list_size(ll);
    FAIL(ll_size != 4,
         "linked_list (#2) size was not equal to 4")
    
    SUBTEST(iterate_over_linked_list_2)
    iter = linked_list_create_iterator(ll, 0);
    for (size_t i = 1; i <= 4; i++) {
        FAIL(iter->data != i,
             "Iterator does not contain correct data for linked_list (#2)")
        FAIL(iter->current_index != (i - 1),
             "Iterator does not contain correct index for linked_list (#2)")

        // Next element.
	//
	linked_list_iterate(iter);
    }
    ll_size = linked_list_size(ll);
    FAIL(ll_size != 4,
         "linked_list (#2) size was not equal to 4")
    linked_list_delete(ll);
    linked_list_delete_iterator(iter);

    SUBTEST(check_insert_function_at_end)
    // Checck that insertion of a single element at an out of bounds
    // index works on an empty linked list.
    //
    ll      = linked_list_create();
    ll_size = linked_list_size(ll);
    FAIL(ll_size != 0,
         "linked_list (#3) size is non-zero when created")
    FAIL(ll == NULL,
         "Failed to create new linked_list (#3)") 

    FAIL(linked_list_insert(ll, 1, 0) != false,
         "Inserted index in empty linked_list (#3) at index 1.")

    // Check insertion at end, same 1-4 test.
    //
    for (size_t i = 0; i < 4 ; i++) {
        bool status = linked_list_insert(ll, i, i + 1);
	FAIL(status != true,
             "linked_list_insert() failed for linked_list (#3) unexpectedly")
    }

    iter = linked_list_create_iterator(ll, 0);
    for (size_t i = 1; i <= 4; i++) {
        FAIL(iter->data != i,
             "Iterator does not contain correct data for linked_list (#3)")
        FAIL(iter->current_index != (i - 1),
             "Iterator does not contain correct index for linked_list (#3)")

	linked_list_iterate(iter);
    }

    linked_list_delete(ll);
    linked_list_delete_iterator(iter);

    SUBTEST(check_insert_function_at_beginning)
    // Check insertion at front, same 4-1 test.
    //
    ll = linked_list_create();
    // Not checking size at this point. It likely works.
    //
    FAIL(ll == NULL,
         "Failed to create new linked_list (#4)")
    for (size_t i = 4; i != 0; i--) {
        bool status = linked_list_insert(ll, 0, i);
	FAIL(status != true,
             "linked_list_insert() failed for linked_list (#4) unexpectedly")
    }

    iter = linked_list_create_iterator(ll, 0);
    for (size_t i = 1; i <= 4; i++) {
        FAIL(iter->data != i,
             "Iterator does not contain correct data for linked_list (#4)")
        FAIL(iter->current_index != (i - 1),
             "Iterator does not contain correct index for linked_list (#4)")

	linked_list_iterate(iter);
    }

    linked_list_delete(ll);
    linked_list_delete_iterator(iter);

    SUBTEST(check_insert_function_at_middle)
    // Insert 1,2,4 and then insert 3.
    //
    ll = linked_list_create();
    FAIL(ll == NULL,
         "Failed to create new linked_list (#5)")
    bool status = linked_list_insert(ll, 0, 1);
    FAIL(status == false,
         "Failed to insert 1 at the beginning of linked_list (#5)")
    status = linked_list_insert(ll, 1, 2);
    FAIL(status == false,
         "Failed to insert 2 at the end of linked_list (#5)")
    status = linked_list_insert(ll, 2, 4);
    FAIL(status == false,
         "Failed to insert 4 at the end of linked_list (#5)")

    status = linked_list_insert(ll, 2, 3);
    FAIL(status == false,
         "Failed to insert 3 at the middle of linked_list (#5)")

    // Standard 1-4 check.
    //
    iter = linked_list_create_iterator(ll, 0);
    for (size_t i = 1; i <= 4; i++) {
        FAIL(iter->data != i,
             "Iterator does not contain correct data for linked_list (#5)")
        FAIL(iter->current_index != (i - 1),
             "Iterator does not contain correct index for linked_list (#5)")

	linked_list_iterate(iter);
    }

    linked_list_delete(ll);
    linked_list_delete_iterator(iter);

    PASS(check_insertion_functionality)
}

void check_find_functionality(void) {
    TEST(check_find_functionality)

    // Single linked_list, find elements.
    //
    struct linked_list * ll = linked_list_create();

    // Create list of ints 1 to 10.
    //
    for (size_t i = 0; i < 10; i++) {
        linked_list_insert_end(ll, i + 1);
    }

    // Find beginning.
    //
    SUBTEST(find_beginning)
    size_t index = linked_list_find(ll, 1);
    FAIL(index != 0,
         "Did not find 1 at beginning of linked_list")
    
    // Find end.
    //
    SUBTEST(find_end)
    index = linked_list_find(ll, 10);
    FAIL(index != 9,
	 "Did not find 10 at end of linked_list")

    // Find middle.
    //
    SUBTEST(find_middle)
    index = linked_list_find(ll, 5);
    FAIL(index != 4,
         "Did not find 5 at end of linked_list")

    // Find non-existant data 
    //
    SUBTEST(find_nonexistant)
    index = linked_list_find(ll, 11);
    FAIL(index != SIZE_MAX,
         "Found 11 when it is not in the linked_list")

    linked_list_insert_end(ll, 6);

    // Ensure the first instance of 6 is found.
    //
    SUBTEST(find_first_of_duplicate_data)
    index = linked_list_find(ll, 6);
    FAIL(index != 5,
         "Failed to find 6, when duplicate added to end of linked_list")

    linked_list_delete(ll);

    PASS(check_find_functionality)
}

int main(void) {
    // Set up signal handler for catching infinite loops.
    //
    signal(SIGALRM, gracefully_exit_on_suspected_infinite_loop);

    // Setup instrumented memory allocation/deallocation.
    //
    linked_list_register_malloc(&instrumented_malloc);
    linked_list_register_free(&free);

    check_null_handling();
    check_empty_list_properties();
    check_insertion_functionality();
    check_find_functionality();

    return 0;
}
