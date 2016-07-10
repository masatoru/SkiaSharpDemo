SkiaSharpのSampleを改変して日本語フォントと縦組みのテストです。

+ 元のSkiaSharpのSampleは下記
  - https://github.com/mono/SkiaSharp/tree/master/samples
+ 実際の描画はDrawTateSampleで、その中のdrawTateで一文字ずつ縦向きに書いてます
+ 和文フォントはIPA明朝を使用(ipaexm.ttf)
  - http://mojikiban.ipa.go.jp/1300.html
  - 使用にはライセンスに同意する必要があります（念のため）
+ 各OS側でフォントファイル(ipaexm.ttf)を指定してます
  - Android MainActivity.OnCreate
  - iOS AppDelegate.FinishedLaunching
+ PrismのテストもかねてPrism.Formsの形式になってます。
+ UWP版は現在動作しません

### 追記
+ SkiaSharp 1.49.4betaでSKPaint.IsTextVerticalが追加された
  - 組向きは縦になるがイメージは横向きのまま（使えない）

### 現在の悩み
+ 縦組みのイメージで表示したい
  - 句読点、ぁゃっなどが現在は横組みのイメージ
  - SKTypeface.FromFileでフォントファイルを指定してるけどここらへんで「縦組み用」みたいな指定ができればなぁ
