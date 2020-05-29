Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Public Class language_package
    Public name As String
    Public regex_vowels As String
    Public regex_consonants As String
    Public package_pretrans_map As String
    Public package_main_map As String
End Class
Public Class maglish
    Public language_class As New language_package
    Public constants As String = "(ssa)|(rra)|(tth)|((n|N)ch(a)?)|(b(h)?)|(B(h)?)|(c(h(h)?)?)|(C(h)?)|(d(h)?)|(D(h)?)|(f|F)|(g(h)?)|(G)|(h)|(j(h)?)|(J(h)?)|(k(h)?)|(K)|(l)|(L)|(m|M)|(N(t)?)|(n(g|j|k|n|t(h)?)?)|(ph?)|(P)|(q)|(Q)|(r)|(R)|(s(h)?)|(S)|(t(t|h(h)?)?)|(T(h)?)|(v|V)|(w|W)|(x|X)|(y|Y)|(z(h)?)|(Z)" '"(b(h)?)|(B(h)?)|(c(h(h)?)?)|(C(h)?)|(d(h)?)|(D(h)?)|(f|F)|(g(h)?)|(G)|(h)|(j(h)?)|(J(h)?)|(k(h)?)|(K)|(l)|(L)|(m|M)|(N(t)?)|(n(g|j|k|n|t(h)?)?)|(ph?)|(P)|(q)|(Q)|(r)|(R)|(s(h)?)|(S)|(t(t|h(h)?)?)|(T(h)?)|(v|V)|(w|W)|(x|X)|(y|Y)|(z(h)?)|(Z)"
    Public vowels As String = "(nt(a|e))|(a(a|e|i|u)?)|(A|U|E|O|I|H)|(i(i)?)|(e(e|i)?)|(u(u)?)|(o(o|a|u)?)|(r~|r_|r |r`)|(R~|R_|R |R`)|(m_|(m\s))|(n_|n )|(N_|N )|(l_|l )|(L_|L )|(_|~)"
    Public dict As New Dictionary(Of String, String)
    Public pre_trans_dict As New Dictionary(Of String, String)
    Public init_stat = False
    Public pre_trans_file As String = "malayalam\map\pre_trans.map"
    Public map_file As String = "malayalam\map\mal.map"

    Public Sub load_package(j As String)

        If My.Computer.FileSystem.FileExists(Application.StartupPath.ToString + "\packages\" + j) Then
            Dim text As New System.IO.StreamReader(Application.StartupPath.ToString + "\packages\" + j)
            Dim k = JsonConvert.DeserializeObject(text.ReadToEnd)
            language_class.name = (k("name"))
            language_class.regex_vowels = (k("regex_vowels"))
            language_class.regex_consonants = (k("regex_consonants"))
            language_class.package_pretrans_map = (k("package_pretrans_map"))
            language_class.package_main_map = (k("package_main_map"))
            text.Close()
        Else
            Exit Sub
        End If

        'assuming every thing loaded properly
        Dim texti As New System.IO.StreamReader(Application.StartupPath.ToString + "\packages\" + language_class.regex_consonants)
        constants = (texti.ReadToEnd.ToString)
        texti.Close()
        Dim textj As New System.IO.StreamReader(Application.StartupPath.ToString + "\packages\" + language_class.regex_vowels)
        vowels = (textj.ReadToEnd.ToString)
        textj.Close()
        pre_trans_file = language_class.package_pretrans_map
        map_file = language_class.package_main_map
    End Sub

    Public Sub init()
        Dim s As New System.IO.StreamReader(Application.StartupPath.ToString + "\packages\" + map_file)
        Dim prase As String
        Dim ms As Integer = 1
        While s.Peek <> -1
            prase = s.ReadLine
            Dim sr() As String
            If (prase(0) <> "#" And prase(0) <> " " And prase(0) <> "-") Then
                sr = prase.Split(":")
                If (dict.ContainsKey(sr(0))) Then
                    ''handel same keys here 
                Else
                    Me.dict.Add(sr(0), sr(1))
                End If
            End If
            ms += 1
        End While
        s.Close()
        init_stat = True
    End Sub
    Public Sub init_pretrans()
        Dim s As New System.IO.StreamReader(Application.StartupPath.ToString + "\packages\" + pre_trans_file)
        Dim prase As String
        Dim ms As Integer = 1
        While s.Peek <> -1
            prase = s.ReadLine
            Dim sr() As String
            If (prase.Length > 0) Then
                If (prase(0) <> "#" And prase(0) <> " " And prase(0) <> "-") Then
                    sr = prase.Split(":")
                    If (pre_trans_dict.ContainsKey(sr(0))) Then
                        ''handel same keys here 
                    Else
                        Me.pre_trans_dict.Add(sr(0), sr(1))
                    End If
                End If
                ms += 1
            End If

        End While
        s.Close()
        init_stat = True
    End Sub

    Public Function split_word(word As String) As ArrayList
        Dim syllables As New ArrayList
        Dim vowel_start_p = True
        Dim re As New Regex(vowels)
        Dim rec As New Regex(constants)
        While (word.Length)
            If re.Match(word).Success And re.Match(word).Index = 0 Then
                Dim matches = re.Matches(word).Item(0).ToString
                If vowel_start_p Then
                    syllables.Add("~" + matches)
                Else
                    syllables.Add(matches)
                End If
                vowel_start_p = True
                word = word.Substring(matches.Length)
            ElseIf rec.Match(word).Success Then
                If (rec.Match(word).Index = 0) Then
                    Dim matches = rec.Matches(word).Item(0).ToString
                    syllables.Add(matches)
                    vowel_start_p = False
                    word = word.Substring(matches.Length)
                    If (re.Match(word).Index <> 0 Or word.Length = 0) Then
                        syllables.Add("*")
                    ElseIf re.IsMatch(word) Then
                        matches = re.Matches(word).Item(0).ToString
                        syllables.Add(matches)
                        word = word.Substring(matches.Length)
                    End If
                End If
            End If
        End While
        Return syllables
    End Function

    Public Function match_codes(code As String) As String

        If (Me.dict.ContainsKey(code)) Then
            Return Me.dict(code)
        End If
        Return code
    End Function

    Public Function one_word(word_ow As String) As String
        If pre_trans_dict.ContainsKey(word_ow) And My.Settings.pretrans_map = True Then
            'MsgBox(":)")
            Return pre_trans_dict(word_ow).ToString
        End If
        Dim sylables_ow = split_word(word_ow)
        Dim letter_ow As New ArrayList
        For Each j In sylables_ow
            letter_ow.Add(match_codes(j))
        Next
        Dim er As String = ""
        For Each j In letter_ow
            er += j
        Next
        Return er
    End Function
    Public Function many_words(sentence As String) As String
        Dim pattern = "((" + vowels + ")|(" + constants + "))+"
        Dim words As New ArrayList

        While (sentence.Length >= 1)
            Dim re As New Regex("^``" + pattern)

            If (re.IsMatch(sentence)) Then
                words.Add("`")
                Dim match = re.Matches(sentence).Item(0).ToString
                words.Add(one_word(match.Substring(2)))
                sentence = sentence.Substring(match.Length)
            Else
                re = New Regex("^`" + pattern)

                If (re.IsMatch(sentence)) Then
                    Dim match = re.Matches(sentence).Item(0).ToString
                    words.Add(match.Substring(1))
                    sentence = sentence.Substring(match.Length)
                Else
                    re = New Regex("^" + pattern)

                    If re.IsMatch(sentence) Then
                        Dim Match = re.Matches(sentence).Item(0).ToString
                        words.Add(one_word(Match))
                        sentence = sentence.Substring(Match.Length)
                    Else
                        words.Add(sentence(0))
                        sentence = sentence.Substring(1)
                    End If
                End If
            End If
        End While

        Dim er As String = ""
        For Each j In words
            er += j
        Next
        Return er
    End Function
End Class
