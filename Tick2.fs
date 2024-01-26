module Tick2

//---------------------------Tick2 PartA skeleton code-------------------------------//


module PartACase1 =
    // Three record types, one data value of each type. Choose suitable names.
    
    type MScBoundaries = {
        Distinction: int;
        Merit: int;
        Pass: int;
        Fail: int;
    }
    
    type MEngBoundaries = {
        First: int;
        UpperSecond: int;
        LowerSecond: int;
        Fail: int;
    }

    type BEngBoundaries = {
        First: int;
        UpperSecond: int;
        LowerSecond: int;
        Third: int;
        Fail: int;
    }

    let MScData = {
        Distinction = 70;
        Merit = 60;
        Pass = 50;
        Fail = 0;
    } 

    let MEngData = {
        First = 70;
        UpperSecond = 60;
        LowerSecond = 50;
        Fail = 0;
    }

    let BEngData = {
        First = 70;
        UpperSecond = 60;
        LowerSecond = 50;
        Third = 40;
        Fail = 0;
    }

module PartACase2 =
    // One record type, three data values of this type. Choose suitable names.

    type CourseBoundary = {
        Course: string;
        Bound70: string option;
        Bound60: string option;
        Bound50: string option;
        Bound40: string option;
        Bound0: string;
        // Boundaries: list<{|classification: string; boundary: int|}>;
    }

    let MScData = {
        Course = "MSc";
        Bound70 = Some "Distinction";
        Bound60 = Some "Merit";
        Bound50 = Some "Pass";
        Bound40 = None;
        Bound0 = "Fail";
    }

    let MEngData = {
        Course = "MEng";
        Bound70 = Some "First";
        Bound60 = Some "UpperSecond";
        Bound50 = Some "LowerSecond";
        Bound40 = None;
        Bound0 = "Fail";
    }

    let BEngData = {
        Course = "BEng";
        Bound70 = Some "First";
        Bound60 = Some "UpperSecond";
        Bound50 = Some "LowerSecond";
        Bound40 = Some "Third";
        Bound0 = "Fail";
    }

module PartACase3 =
    // One type, three data values of this type. Choose suitable names.
    let MScBoundaries = ["Distinction", 70; "Merit", 60; "Pass", 50; "Fail", 0]
    let MEngBoundaries = ["First", 70; "UpperSecond", 60; "LowerSecond", 50; "Fail", 0]
    let BEngBoundaries = ["First", 70; "UpperSecond", 60; "LowerSecond", 50; "Third", 40; "Fail", 0]

//---------------------------Tick2 PartB case 2 skeleton code-------------------------------//

module PartBCase2 =

    open PartACase2 // get unqualified access to Case 2 types and values

    /// Return as a Ok string the name of the correct classification for a student
    /// on given course with given mark.
    /// Return Error if course or mark are not possible (marks must be in range 100 - 0). 
    /// The error message should say what the problem in the data was.
    let classify (course: string) (mark: float) : Result<string,string> =
        if mark > 100 || mark < 0 
        then Error <| sprintf "issue with bounds! Got {mark}."
        else
        let getHons (mark: float) (bounds: CourseBoundary): Result<string, string> = 
            match mark with
            | mark when mark >= 70 && Option.isSome bounds.Bound70 -> Ok bounds.Bound70.Value
            | mark when mark >= 60 && Option.isSome bounds.Bound60 -> Ok bounds.Bound60.Value
            | mark when mark >= 50 && Option.isSome bounds.Bound50 -> Ok bounds.Bound50.Value
            | mark when mark >= 40 && Option.isSome bounds.Bound40 -> Ok bounds.Bound40.Value
            | mark when mark >= 0 -> Ok bounds.Bound0
            | _ -> Error "bounds checking should prevent us reaching here..."
            
        match course with
        | "MSc" -> getHons mark MScData
        | "MEng" -> getHons mark MEngData
        | "BEng" -> getHons mark BEngData
        | _ -> Error <| sprintf "could not match course name {course}"

//---------------------------Tick2 PartB case 3 skeleton code-------------------------------//

module PartBCase3 =

    open PartACase3 // get unqualified access to Case 3 types and values

    /// Return as a Ok string the name of the correct classification for a studen on given course with given mark.
    /// Return Error if course or mark are not possible (marks must be in range 100 - 0). The error message should say what the problem in the data was.
    let classify (course: string) (mark: float) : Result<string,string> =
        let rec getHons (mark: float) (bounds: list<string * int>): Result<string, string> =
            match bounds with
            | [] -> Error "No bounds matched."
            | hd::rest when (float (snd hd) > mark) -> getHons mark rest
            | hd::_ -> Ok (fst hd)
        
        if mark > 100 || mark < 0 
        then Error <| sprintf "issue with bounds! Got {mark}."
        else
        
        match course with
        | "MSc" -> getHons mark MScBoundaries
        | "MEng" -> getHons mark MEngBoundaries
        | "BEng" -> getHons mark BEngBoundaries 
        | _ -> Error <| sprintf "could not match course name {course}"


//------------------------------------Tick2 PartC skeleton code-----------------------------------//

module PartC =
    open PartACase3 // get unqualified access to Case 3 types and values
    open PartBCase3 // get unqualified access to classify function

    type Marks = {Mark1: float} // simplified set of marks (just one mark) used for compilation of code

    /// Return the total mark for a student used to determine classification. 
    /// marks:  constituent marks of student on given course.
    /// course: name of course student is on
    /// Return None if the course is not valid or any of the marks are
    /// outside the correct range 0 - 100.
    let markTotal (marks: Marks) (course: string) : float option =
        match course with
        | "MEng"  | "BEng" | "MSc" when marks.Mark1 <= 100.0 && marks.Mark1 >= 0.0 ->
            Some marks.Mark1 // in this case with only one mark, student total is just the mark!
        | _ -> None

    /// Operation:
    /// 1. Return an error if boundary is not a valid boundary for course.
    /// 2. Return IsAboveBoundary = true if total is above or equal to boundary
    /// 3. Return Uplift = Some uplift if total is in the valid possible uplift range (0 - -2.5%) of boundary.
    let upliftFunc 
        (marks: Marks) 
        (boundary:string) 
        (course: string)
            : Result<{|IsAboveBoundary: bool; Uplift:float option|}, string> =
        // Use markTotal to calculate total from marks
        // Also return an error if markTotal fails to calculate a mark
        // Ok return type is an anonymous record see link in WS2.
        // upliftFunc is assumed (when implemented) to take boundary info from a value defined above
        // with whatever data structure is used for it. In Part C you do not implement
        // upliftFunc and so need not consider any of this.
        failwithf "Not Implemented" // do not change - implementation not required

    /// Given a list of boundaries, and a course, and a student's marks:
    /// Return the student classification, or an error message if there is
    /// any error in the data.
    /// boundaries: name only, subfunctions will know boundary marks based on course, 
    /// this function needs only the results of calling its subfunctions.
    let classifyAndUplift 
        (boundaries: string list)
        (course: string) 
        (marks: Marks)
                : Result<string,string> =
        // Use upliftFunc and markTotal and classify.
        // Assume that the student can be within possible uplift range of at most one boundary.
        // Assume that classify is correct unless student is within uplift range of a given boundary,
        // If student is within uplift range of a boundary `boundaryName` work out classification as:
            // let total = markTotal marks course
            // let effectiveMark = total + upliftFunc marks boundaryName course
            // let className = classify course effectiveMark
            // Return Ok classname or an error if there is any error.
            // (option and error returns ignored in above comments, must be dealt with)

        failwithf "Not implemented" // replace by your code ()

//------------------------------Simple test data and functions---------------------------------//
module TestClassify =
    /// test data comaptible with the Tick 2 problem
    let classifyUnitTests = [
        "MEng",75.0, Ok "First"
        "MSc", 75.0,Ok "Distinction"
        "BEng", 75.0, Ok "First"
        "MEng",65.0, Ok "UpperSecond"
        "MSc", 65.0, Ok "Merit"
        "BEng", 65.0, Ok "UpperSecond"        
        "MEng",55.0, Ok "LowerSecond"
        "MSc", 55.0, Ok "Pass"
        "BEng", 55.0, Ok "LowerSecond"        
        "MEng",45.0, Ok "Fail"
        "MSc", 45.0, Ok "Fail"
        "BEng", 45.0, Ok "Third"
        "BEng", 35.0, Ok "Fail"        
    ]

    let runClassifyTests unitTests classify testName =
        unitTests
        |> List.map (fun (data as (course,mark,_)) -> classify course mark, data)
        |> List.filter (fun (actualClass, (_,_,className)) -> actualClass <> className)
        |> function 
            | [] -> printfn $"all '{testName}' tests passed."
            | fails -> 
                fails 
                |> List.iter (fun (actual, (course,mark,className)) 
                                -> printfn $"Test Failed: {course}, {mark}, expected className={className}, \
                                          actual className={actual}")


//-------------------------------------------------------------------------------------------//
//---------------------------------Run Part B tests------------------------------------------//
//-------------------------------------------------------------------------------------------//

open TestClassify
let runTests() =
    runClassifyTests classifyUnitTests PartBCase2.classify "Case2"
    runClassifyTests classifyUnitTests PartBCase3.classify "Case3"


//-------------------------------------------------------------------------------------------//
//---------------------------------Tick2 Part X Skeleton code--------------------------------//
//-------------------------------------------------------------------------------------------//
module PartX =
    type Lens<'A,'B> = ('A -> 'B) * ('B -> 'A -> 'A)

    let lensMap (lens: Lens<'A,'B>) (f: 'B -> 'B) (a: 'A) =       
        (fst lens a |> f |> snd lens) a

    let mapCAndB (lensC: Lens<'A,'C>) (lensB: Lens<'A,'B>) (fc:'C->'C) (fb: 'B->'B) =
        lensMap lensC fc >> lensMap lensB fb

    let combineLens (l1: Lens<'A,'B>) (l2: Lens<'B,'C>) : Lens<'A,'C> =
        failwithf "not implemented yet" // replace with your definition
