### SkiaSharpDemo
SkiaSharpのSampleを改変して日本語フォントと縦組みのテスト

### メモなど
+ 元のSkiaSharpのSampleは下記
  - https://github.com/mono/SkiaSharp/tree/master/samples
+ 和文フォントはIPA明朝を使用(ipaexm.ttf)
  - http://mojikiban.ipa.go.jp/1300.html
  - 使用にはライセンスに同意する必要があります（念のため）
+ 各OS側でフォントファイルを指定してます
  - Android MainActivity.OnCreate
  - iOS AppDelegate.FinishedLaunching
+ PrismのテストもかねてPrism.Formsの形式になってます。

### 現在の悩み
+ 縦組みのイメージで表示したい
  - 句読点、ぁゃっなどが現在は横組みのイメージ
  - SKTypeface.FromFileでフォントファイルを指定してるけどここらへんで「縦組み用」みたいな指定ができればなぁ
