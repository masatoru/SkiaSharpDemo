### SkiaSharpDemo
SkiaSharpのsampleを使って日本語フォント、縦組みのテスト
https://github.com/mono/SkiaSharp/tree/master/samples

+ IPA明朝 http://mojikiban.ipa.go.jp/1300.html
  - ライセンスに同意する必要があります（念のため）
+ 各OS側でフォントファイルを指定してます
  - Android MainActivity.OnCreate
  - iOS AppDelegate.FinishedLaunching
+ PrismのテストもかねてPrism.Formsの形式になってます。

### 現在の悩み
+ 縦組みのイメージで表示したい
  - 句読点、ぁゃっなど
