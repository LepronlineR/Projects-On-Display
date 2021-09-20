COMMANDS

input a text file that contains: (example file inputs included)

genome [filename of genome]
[table size]
[occupancy]
[kmer]
[query] [mismatches] [genome sequence]...

========================================================================
                            sample input command
========================================================================
    genome genome_small.txt
    kmer 10
    start_timer hashtable
    query 2 TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    query 2 TTGACCTTTGGTTAACCCCTCCCTTGAAGGTGAAGCTTGTAAA
    query 2 AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    query 2 TCTTGTACTTATTCTCCAATTCAGTCACAGGCCTTGTGGGCTACCCTTCA
    query 5 TTTTTTTTTTTTTTTCTTTTTT
    stop_timer
    start_timer vector
    vector_query 2 TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    vector_query 2 TTGACCTTTGGTTAACCCCTCCCTTGAAGGTGAAGCTTGTAAA
    vector_query 2 AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    vector_query 2 TCTTGTACTTATTCTCCAATTCAGTCACAGGCCTTGTGGGCTACCCTTCA
    vector_query 5 TTTTTTTTTTTTTTTCTTTTTT
    stop_timer
    timer_table
    quit
========================================================================

========================================================================
                            sample input output
========================================================================
    Query: TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    504 0 TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    Query: TTGACCTTTGGTTAACCCCTCCCTTGAAGGTGAAGCTTGTAAA
    5002 2 TTGACCTTTGGTTAACCAATCCCTTGAAGGTGAAGCTTGTAAA
    Query: AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    4372 0 AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    Query: TCTTGTACTTATTCTCCAATTCAGTCACAGGCCTTGTGGGCTACCCTTCA
    No Match
    Query: TTTTTTTTTTTTTTTCTTTTTT
    4428 0 TTTTTTTTTTTTTTTCTTTTTT
    4429 3 TTTTTTTTTTTTTTCTTTTTTG
    4430 4 TTTTTTTTTTTTTCTTTTTTGA
    4431 5 TTTTTTTTTTTTCTTTTTTGAG
    Query: TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    504 0 TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    Query: TTGACCTTTGGTTAACCCCTCCCTTGAAGGTGAAGCTTGTAAA
    5002 2 TTGACCTTTGGTTAACCAATCCCTTGAAGGTGAAGCTTGTAAA
    Query: AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    4372 0 AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    Query: TCTTGTACTTATTCTCCAATTCAGTCACAGGCCTTGTGGGCTACCCTTCA
    No Match
    Query: TTTTTTTTTTTTTTTCTTTTTT
    4428 0 TTTTTTTTTTTTTTTCTTTTTT
    4429 3 TTTTTTTTTTTTTTCTTTTTTG
    4430 4 TTTTTTTTTTTTTCTTTTTTGA
    4431 5 TTTTTTTTTTTTCTTTTTTGAG
========================================================================

HASH FUNCTION DESCRIPTION

    DJB Hash Function

    An algorithm produced by Professor Daniel J. Bernstein 
    and shown first to the world on the usenet newsgroup comp.lang.c. 
    It is one of the most efficient hash functions ever published.

    Source: https://www.partow.net/programming/hashfunctions/#top

    I chose this function because it does say that it is one of the most efficient hash functions
    ever published, and I trust that it is true.

HASH TABLE IMPLEMENTATION

    A key is generated from a starting index, and the k_mer size, then the index is calculated
    from the hash function mod table size. Then the occupancy will be checked before inserted. If the
    occupancy of the table is greater than the clamp occupancy, then the table will be resized.
    When inserting, I utilized linear probing to solve my collisions. Therefore, once the index is blocked
    it will go through the entire table to see if there is any free spaces.

    The hash table is a: std::vector<std::pair<std::string, std::list<unsigned int> > >
    where the KEY is the genome represented by the clamped k_mer amount, and that key leads to the positions
    of when that genome has appeared.
        - so a table can be < (ACACAC, {4, 200, 1999}), (CCACGT, {50, 999}), ... >

    The hash table resizes when the current occupancy of the table is greater than the expected occupancy.
    It resizes by creating a new table with double the size of the old table. Then iterates through the
    old table, getting the new index of the new table, and inserting the index value inside the iterator.
    Linear probing is applied to index transferring, when a collision may occur.

ANALYSIS OF PERFORMANCE OF THE ALGORITHM:

L = length of the genome sequence
q - query length
p - average number of different locations of where key is found
k - key size (k-mer size)

How much memory will the hash table data structure require (order notation for memory use)?

N = p*k^4 (total size of the table)
    - this is because for every different key, there are 4 different options (ATCG)
    that fits in the total amount of spaces notated by the size of kmer.
    - [_][_][_][_] kmer = 4. In the sequence, there are 4 different choices for each spot

    - HERE "N" is SUBSTITUTED for p*k^4 in the ORDER NOTATION

O(p*k^4), for every different key, the hash table will create that bucket for that key.
Therefore, the total space would be the total amount of different keys, which depend on 
the difference in kmers. This makes sense because as k increases, the table will eventually
become larger, whereas every key can have a higher amount of different combination of (ATCG).

What is the order notation for performance (running time) of each of
the commands? What about for the hash table's insert, find, and resize?

i.e. findQuery (find), insert (insert), resize (resize)

N = p*k^4 (total size of the table)

|-------------------------------------------------------------------|
|  createTable: O(l-k) average case, O(N*(l-k)) worst case          |
|                                                                   |
|  -  creates table by assigning key for each position in the       |
|     (genome sequence - key size), then inserting or resizing      |
|     if possible. Worst case happens when both inserting reaches   |
|     it's worst case, and when the table has to resize.	    |
|-------------------------------------------------------------------|
|  findQuery: O(p*q) average case, O(p*q*N) worst case              |
|                                                                   |
|   - finds the location based on getIndexes(), and then goes       |
|     through the average number of different locations of where    |
|     the key is found, and compare the mismatches. Worst case is   |
|     when getIndexes has their worst case                          |
|-------------------------------------------------------------------|
|  compareMismatches: O(q)                                          |
|                                                                   |
|   - goes through the location of the genome clamped by the        |
|     length of the query, iterates through the length of that      |
|     genome sequence by query length, and counts the mismatches.   |
|     Once the assigned mismatch is above the genome mismatch, the  |
|     compareMismatches will return the total mismatches.           |
|-------------------------------------------------------------------|
|  getIndexes: O(1) average case, O(N) worst case                   |
|                                                                   |
|   - gets the index through hash function and table size, then     |
|     looks at the index. If the index is not similar to the key,   |
|     then the index iterates through the table to find the key.    |
|     The worst case is when the index iterates through the entire  |
|     table to find the key.					    |
|-------------------------------------------------------------------|
|  resize: O(N)                                                     |
|                                                                   |
|  -  creates a new table and then places the new index inside of   |
|     the new table from the old table. Eventually, when the        |
|     table will resize enough, it will become the size of the      |
|     hash table, resulting in a O(N).                              |
|-------------------------------------------------------------------|
|  insert: O(1) average case, O(N) worst case                       |
|                                                                   |
|  - if the index is empty, it will be inserted, for linear probing |
|    a key can potentially be 'p' positions apart from it's initial.|
|    Therefore, the worst case should be the index iterating        |
|    through the entire table. 					    |
|-------------------------------------------------------------------|

there are a few extra commands:

vector_query N sequence:
    - similar to the query command, except if you want to run a query with a vector,
    then it will do that
start_timer (string):
    - start a timer and then label that timer with that string
stop_timer:
    - stop the timer, and then put it inside a timer table
timer_table:
    - prints out the timer table, which includes all recorded times 

NOTE!!!!:
    - start_timer must be called before stop_timer

    - the timer acts as a single stopwatch, it cant be called multiple times
        - i.e. you cant do start_timer start_one, and then call start_timer start_two
        the first timer (start_one) needs to end.
        Calling start_timer start_one, stop_timer, and then call start_timer start_two,
        stop_timer, works. 

    - The first query call is always longer because when it is called, it constructs the table
    
    - I included extra test files for small, medium, and custom to test out the timer. Please run all of it :)

    - input_custom takes longer since the vector construction is ~50 seconds


========================================================================
                            sample input command
========================================================================
    genome genome_small.txt
    kmer 10
    start_timer hashtable
    query 2 TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    query 2 TTGACCTTTGGTTAACCCCTCCCTTGAAGGTGAAGCTTGTAAA
    query 2 AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    query 2 TCTTGTACTTATTCTCCAATTCAGTCACAGGCCTTGTGGGCTACCCTTCA
    query 5 TTTTTTTTTTTTTTTCTTTTTT
    stop_timer
    start_timer vector
    vector_query 2 TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    vector_query 2 TTGACCTTTGGTTAACCCCTCCCTTGAAGGTGAAGCTTGTAAA
    vector_query 2 AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    vector_query 2 TCTTGTACTTATTCTCCAATTCAGTCACAGGCCTTGTGGGCTACCCTTCA
    vector_query 5 TTTTTTTTTTTTTTTCTTTTTT
    stop_timer
    timer_table
    quit
========================================================================

========================================================================
                            sample input output
========================================================================
    Query: TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    504 0 TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    Query: TTGACCTTTGGTTAACCCCTCCCTTGAAGGTGAAGCTTGTAAA
    5002 2 TTGACCTTTGGTTAACCAATCCCTTGAAGGTGAAGCTTGTAAA
    Query: AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    4372 0 AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    Query: TCTTGTACTTATTCTCCAATTCAGTCACAGGCCTTGTGGGCTACCCTTCA
    No Match
    Query: TTTTTTTTTTTTTTTCTTTTTT
    4428 0 TTTTTTTTTTTTTTTCTTTTTT
    4429 3 TTTTTTTTTTTTTTCTTTTTTG
    4430 4 TTTTTTTTTTTTTCTTTTTTGA
    4431 5 TTTTTTTTTTTTCTTTTTTGAG
    Query: TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    504 0 TATTACTGCCATTTTGCAGATAAGAAATCAGAAGCTC
    Query: TTGACCTTTGGTTAACCCCTCCCTTGAAGGTGAAGCTTGTAAA
    5002 2 TTGACCTTTGGTTAACCAATCCCTTGAAGGTGAAGCTTGTAAA
    Query: AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    4372 0 AAACACACTGTTTCTAATTCAGGAGGTCTGAGAAGGGA
    Query: TCTTGTACTTATTCTCCAATTCAGTCACAGGCCTTGTGGGCTACCCTTCA
    No Match
    Query: TTTTTTTTTTTTTTTCTTTTTT
    4428 0 TTTTTTTTTTTTTTTCTTTTTT
    4429 3 TTTTTTTTTTTTTTCTTTTTTG
    4430 4 TTTTTTTTTTTTTCTTTTTTGA
    4431 5 TTTTTTTTTTTTCTTTTTTGAG
    --------------------------------------------------
    | hashtable              |	0.0072232 second(s)
    --------------------------------------------------
    | vector                 |	0.200697 second(s)
    --------------------------------------------------
========================================================================