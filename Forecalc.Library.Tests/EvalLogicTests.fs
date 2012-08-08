﻿module EvalLogicTests

open NUnit.Framework
open FsUnit
open Forecalc.Library
open Forecalc.Library.Ast
open Forecalc.Library.Eval

[<Test>]
let ``Eq(String "42", String "42") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Eq(String "42", String "42")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Eq(Boolean false, String "42") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Eq(Boolean false, String "42")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Eq(Error("#REF!", String "42") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Eq(Error "#REF!", String "42")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Eq(String "42", Error("#REF!") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Eq(String "42", Error "#REF!")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Eq(String "42", EscapedString("42") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Eq(String "42", EscapedString "42")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``NotEq(String "42", String "42") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = NotEq(String "42", String "42")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``NotEq(Boolean false, String "42") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = NotEq(Boolean false, String "42")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``NotEq(Error("#REF!", String "42") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = NotEq(Error "#REF!", String "42")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``NotEq(String "42", Error("#REF!") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = NotEq(String "42", Error "#REF!")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``NotEq(String "42", EscapedString("42") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = NotEq(String "42", EscapedString "42")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Lt(Float 0.0, Float 42.0) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(Float 0.0, Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not String<Float -> Lt(String "42", Float 42.0) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(String "42", Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Float<String -> Lt(Float 42.0, String "42") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(Float 42.0, String "42")
    eval cell expr workbook |> should equal (BooleanValue true)
 
[<Test>]
let ``String<Bool -> Lt(String "42", Boolean true) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(String "42", Boolean true)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not Bool<String -> Lt(Boolean true, String "42") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(Boolean true, String "42")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``false<true -> Lt(Boolean false, Boolean true) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(Boolean false, Boolean true)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not true<false -> Lt(Boolean true, Boolean false) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(Boolean true, Boolean false)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Lt(Error("#REF!", String "42") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(Error "#REF!", String "42")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Lt(String "42", Error("#REF!") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(String "42", Error "#REF!")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Gently<Dirk -> Lt(String "Gently", String("Dirk") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(String "Gently", String "Dirk")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Dirk<Gently -> Lt(String "Dirk", String("Gently") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lt(String "Dirk", String "Gently")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Gt(Float 0.0, Float 42.0) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(Float 0.0, Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``String>Float -> Gt(String "42", Float 42.0) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(String "42", Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not Float>String -> Gt(Float 42.0, String "42") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(Float 42.0, String "42")
    eval cell expr workbook |> should equal (BooleanValue false)
 
[<Test>]
let ``Not String>Bool -> Gt(String "42", Boolean true) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(String "42", Boolean true)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Bool>String -> Gt(Boolean true, String "42") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(Boolean true, String "42")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not false>true -> Gt(Boolean false, Boolean true) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(Boolean false, Boolean true)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``true>false -> Gt(Boolean true, Boolean false) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(Boolean true, Boolean false)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Gt(Error("#REF!", String "42") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(Error "#REF!", String "42")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Gt(String "42", Error("#REF!") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(String "42", Error "#REF!")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Gently>Dirk -> Lt(String "Gently", String("Dirk") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(String "Gently", String "Dirk")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not Dirk>Gently -> Gt(String "Dirk", String("Gently") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gt(String "Dirk", String "Gently")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Lte(Float 0.0, Float 42.0) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(Float 0.0, Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not String<=Float -> Lte(String "42", Float 42.0) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(String "42", Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Float<=String -> Lte(Float 42.0, String "42") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(Float 42.0, String "42")
    eval cell expr workbook |> should equal (BooleanValue true)
 
[<Test>]
let ``String<=Bool -> Lte(String "42", Boolean true) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(String "42", Boolean true)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not Bool<=String -> Lte(Boolean true, String "42") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(Boolean true, String "42")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``false<=true -> Lte(Boolean false, Boolean true) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(Boolean false, Boolean true)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not true<=false -> Lte(Boolean true, Boolean false) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(Boolean true, Boolean false)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Lte(Error("#REF!", String "42") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(Error "#REF!", String "42")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Lte(String "42", Error("#REF!") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(String "42", Error "#REF!")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Gently<=Dirk -> Lte(String "Gently", String("Dirk") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(String "Gently", String "Dirk")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Dirk<=Gently -> Lte(String "Dirk", String("Gently") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(String "Dirk", String "Gently")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Dirk Gently<=dirk gently -> Lte(String "Dirk Gently", String("dirk gently") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(String "Dirk Gently", String "dirk tently")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``42<=42 -> Lte(Float 42.0, Float 42.0) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Lte(Float 42.0, Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Gte(Float 0.0, Float 42.0) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(Float 0.0, Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``String>=Float -> Gte(String "42", Float 42.0) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(String "42", Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not Float>=String -> Gte(Float 42.0, String "42") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(Float 42.0, String "42")
    eval cell expr workbook |> should equal (BooleanValue false)
 
[<Test>]
let ``Not String>=Bool -> Gte(String "42", Boolean true) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(String "42", Boolean true)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Bool>=String -> Gte(Boolean true, String "42") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(Boolean true, String "42")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not false>=true -> Gte(Boolean false, Boolean true) -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(Boolean false, Boolean true)
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``true>=false -> Gte(Boolean true, Boolean false) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(Boolean true, Boolean false)
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Gte(Error("#REF!", String "42") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(Error "#REF!", String "42")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Gte(String "42", Error("#REF!") -> ErrorValue("#REF!")``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(String "42", Error "#REF!")
    eval cell expr workbook |> should equal (ErrorValue "#REF!")

[<Test>]
let ``Gently>=Dirk -> Lt(String "Gently", String("Dirk") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(String "Gently", String "Dirk")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``Not Dirk>=Gently -> Gte(String "Dirk", String("Gently") -> BooleanValue(false)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(String "Dirk", String "Gently")
    eval cell expr workbook |> should equal (BooleanValue false)

[<Test>]
let ``Dirk Gently>=dirk gently -> Gte(String "Dirk Gently", String("dirk gently") -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(String "Dirk Gently", String "dirk gently")
    eval cell expr workbook |> should equal (BooleanValue true)

[<Test>]
let ``42>=42 -> Gte(Float 42.0, Float 42.0) -> BooleanValue(true)``() =
    let workbook = QT4.create<CellContent>()
    let cell = { Sheet = "Sheet1" ; Row = 1 ; Col = 1 }
    let expr = Gte(Float 42.0, Float 42.0)
    eval cell expr workbook |> should equal (BooleanValue true)