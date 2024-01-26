// // For more information see https://aka.ms/fsharp-console-apps
// printfn "Hello from F#"

// type Person = {
//   Age: int;
//   FirstName: string;
//   FamilyName: string;
//   Email: string;
// }

// let newEmail email oldPerson = {oldPerson with Email = email}

// let calcHyp(x,y) =
//   (x ** 2.0 + y ** 2.0) |> sqrt

// type InnerRecordType = {
//   X: int
//   Y: int
// }

// type RecordType = {
//   Q: InnerRecordType
//   R: InnerRecordType
// }

// let data = {
//   Q={X=1;Y=2}
//   R={X=3;Y=4}
// }

// // 4
// type Lens<'A, 'B> = ('A -> 'B) * ('B -> 'A -> 'A)
// type Mylens = Lens<RecordType, InnerRecordType>

// // 5
// let lensMap (lens: Lens<'A, 'B>) (f: 'B -> 'B) (a: 'A) =
//   let getb = fst lens
//   let putb = snd lens
//   let b = getb a
//   let b' = f b
//   putb b' a

// // 6
// let mapCAndB (lensC: Lens<'A, 'C>) (lensB: Lens<'A, 'B>) (fc: 'C -> 'C) (fb: 'B -> 'B) (a: 'A) =
//   // let getC = fst lensC
//   // let putC = snd lensC
//   // let getB = fst lensB
//   // let putB = snd lensB
//   // let c = getC a
//   // let b = getB a
//   // putC c fc
//   // putB b fb

//   a |> lensMap lensC fc |> lensMap lensB fb

// // 7
// module Tick2X =
//   open System
//   type Lens<'A, 'B> = ('A -> 'B) * ('B -> 'A -> 'A)

//   let combineLens (l1: Lens<'A, 'B>) (l2: Lens<'B,'C>) : Lens<'A, 'C> = 
//     // let combRead: 'A -> 'C = fun a -> fst l1 a |> fst l2
//     let combRead: 'A -> 'C = fun a -> fst l2 (fst l1 a)
//     let combUpdate: 'C -> 'A -> 'A = fun c a -> snd l1 ((snd l2) c (fst l1 a)) a

//     combRead, combUpdate

// // 8
// let lst1 = [1 ; -1 ; 6 ; 0 ; -3]
// let lst2 = [2 ; 5 ; 65 ; 3]

// // let firstNegative = List.tryFind ((>) 0) // Why is this not <?
// let firstNegative = (fun x -> 0 > x) |> List.tryFind // Why is this not <?

// let res1 = firstNegative lst1
// let res2 = firstNegative lst2

// // 11
// let firstNegativeFallback (a: int list): int =
//   List.tryFind ((>) 0) a
//   |> Option.orElse (List.tryFind ((<) 0) a)
//   |> Option.defaultValue 0

// // 12
// let rec filter condition lst =
//   match lst with
//   | hd::rest when (condition hd) -> hd :: (filter condition rest)
//   | _::rest -> filter condition rest
//   | [] -> []

// // 13
// let factorial n =
//   let lst = [1..n]
//   List.fold (*) 1 lst

// // 14
// let reverse lst =
//   List.fold (fun a b -> b :: a) [] lst

// // skipped 16 through 20 

// // 21
module Program

Tick2.runTests()