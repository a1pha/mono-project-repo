#include "linked_list.h"

// Function pointers to (potentially) custom malloc() and
// free() functions.
//
static void * (*malloc_fptr)(size_t size) = NULL;
static void   (*free_fptr)(void* addr)    = NULL; 

// Creates a new linked_list.
// PRECONDITION: Register malloc() and free() functions via the
//               linked_list_register_malloc() and 
//               linked_list_register_free() functions.
// POSTCONDITION: An empty linked_list has its head point to NULL.
// Returns a new linked_list on success, NULL on failure.
//
struct linked_list * linked_list_create(void) {
    struct linked_list * created_list = malloc_fptr(sizeof(struct linked_list));
    if (created_list == NULL) {
        return NULL;
    }
    created_list->head = NULL;
    return created_list;
}

// Deletes a linked_list and frees all memory assoicated with it.
// \param ll : Pointer to linked_list to delete
// Returns TRUE on success, FALSE otherwise.
//
bool linked_list_delete(struct linked_list * ll) {
    if (ll == NULL) {
        return false;
    }

    struct node * current = ll->head;
    while (current != NULL) {
        struct node * next = current->next;
        free_fptr(current);
        current = next;
    }

    free_fptr(ll);
    return true;
}

// Returns the size of a linked_list.
// \param ll : Pointer to linked_list.
// Returns size on success, SIZE_MAX on failure.
//
size_t linked_list_size(struct linked_list * ll) {
    if (ll == NULL) {
        return SIZE_MAX;
    }
    size_t size = 0;
    struct node * current = ll->head;
    while (current != NULL) {
        size++;
        current = current->next;
    }
    return size;
}

// Inserts an element at the end of the linked_list.
// \param ll   : Pointer to linked_list.
// \param data : Data to insert.
// Returns TRUE on success, FALSE otherwise.
//
bool linked_list_insert_end(struct linked_list * ll,
                            unsigned int data) {
    if (ll == NULL) {
        return false;
    }

    struct node * new_node = malloc_fptr(sizeof(struct node));
    if (new_node == NULL) {
        return false;
    }
    new_node->data = data;
    new_node->next = NULL;

    struct node * current = ll->head;

    if (current == NULL) {
        ll->head = new_node;
        return true;
    }

    while (current->next != NULL) {
        current = current->next;
    }

    current->next = new_node;
    return true;
}

// Inserts an element at the front of the linked_list.
// \param ll   : Pointer to linked_list.
// \param data : Data to insert.
// Returns TRUE on success, FALSE otherwise.
//
bool linked_list_insert_front(struct linked_list * ll,
                              unsigned int data) {
    if (ll == NULL) {
        return false;
    }

    struct node * new_node = malloc_fptr(sizeof(struct node));
    if (new_node == NULL) {
        return false;
    }
    new_node->data = data;
    new_node->next = ll->head;
    ll->head = new_node;
    return true;
}

// Inserts an element at a specified index in the linked_list.
// \param ll    : Pointer to linked_list.
// \param index : Index to insert data at.
// \param data  : Data to insert.
// Returns TRUE on success, FALSE otherwise.
//
bool linked_list_insert(struct linked_list * ll,
                        size_t index,
                        unsigned int data) {

    if (ll == NULL) {
        return false;
    }

    if (index == 0) {
        return linked_list_insert_front(ll, data);
    }

    struct node * current = ll->head;
    if (current == NULL) {
        return false;
    }

    for (size_t i = 0; i < index - 1; ++i) {
        if (current == NULL) {
            return false;
        }
        current = current->next;
    }

    struct node * new_node = malloc_fptr(sizeof(struct node));
    if (new_node == NULL) {
        return false;
    }

    new_node->data = data;
    new_node->next = current->next;
    current->next = new_node;

    return true;
}

// Finds the first occurrence of data and returns its index.
// \param ll   : Pointer to linked_list.
// \param data : Data to find.
// Returns index of the first index with that data, SIZE_MAX otherwise.
//
size_t linked_list_find(struct linked_list * ll,
                        unsigned int data) {
    if (ll == NULL || ll->head == NULL) {
        return SIZE_MAX;
    }

    struct node * current = ll->head;
    size_t index = 0;
    while (current != NULL) {
        if (current->data == data) {
            return index;
        }
        current = current->next;
        ++index;
    }
    return SIZE_MAX;
}

// Removes a node from the linked_list at a specific index.
// \param ll    : Pointer to linked_list.
// \param index : Index to remove node.
// Returns TRUE on success, FALSE otherwise.
//
bool linked_list_remove(struct linked_list * ll,
                        size_t index) {
    if (ll == NULL || ll->head == NULL) {
        return false;
    }

    struct node * current = ll->head;
    if (index == 0) {
        ll->head = current->next;
        free_fptr(current);
        return true;
    }

    for (size_t i = 0; i < index - 1; ++i) {
        if (current == NULL) {
            return false;
        }
        current = current->next;
    }

    struct node * nodeToDelete = current->next;
    if (nodeToDelete == NULL){
        return false;
    }

    current->next = nodeToDelete->next;
    free_fptr(nodeToDelete);
    return true;
}

// Creates an iterator struct at a particular index.
// \param linked_list : Pointer to linked_list.
// \param index       : Index of the linked list to start at.
// Returns pointer to an iterator on success, NULL otherwise.
//
struct iterator * linked_list_create_iterator(struct linked_list * ll,
                                              size_t index) {
    if (ll == NULL || ll->head == NULL){
        return NULL;
    }

    struct node * current = ll->head;
    for (size_t i = 0; i < index; ++i) {
        if (current == NULL) {
            return NULL;
        }
        current = current->next;
    }
    struct iterator * iter = malloc_fptr(sizeof(struct iterator));
    iter->ll = ll;
    iter->current_node = current;
    iter->current_index = index;
    iter->data = current->data;
    return iter;
}

// Deletes an iterator struct.
// \param iterator : Iterator to delete.
// Returns TRUE on success, FALSE otherwise.
//
bool linked_list_delete_iterator(struct iterator * iter) {
    if (iter == NULL){
        return false;
    }
    free_fptr(iter);
    return true;
}

// Iterates to the next node in the linked_list.
// \param iterator: Iterator to iterate on.
// Returns TRUE when next node is present, FALSE once end of list is reached.
//
bool linked_list_iterate(struct iterator * iter) {
    if (iter == NULL){
        return false;
    }

    if (iter->current_node->next == NULL){
        return false;
    }
    
    iter->current_node = iter->current_node->next;
    ++iter->current_index;
    iter->data = iter->current_node->data;
    return true;
}

// Registers malloc() function.
// \param malloc : Function pointer to malloc()-like function.
// Returns TRUE on success, FALSE otherwise.
//
bool linked_list_register_malloc(void * (*malloc)(size_t)) {
    if (malloc == NULL) {
        return false;
    }
    malloc_fptr = malloc;
    return true;
}

// Registers free() function.
// \param free : Function pointer to free()-like function.
// Returns TRUE on success, FALSE otherwise.
//
bool linked_list_register_free(void (*free)(void*)) {
    if (free == NULL) {
        return false;
    }
    free_fptr = free;
    return true;
}
