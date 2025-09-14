namespace CreateHagenMorphDb

type TypeRec =
        | Error = 0
        | Lemma = 1
        | Word = 2
        | SubWord = 3

module TagData =
       
        let PosTagDataArr :MainTagDict array = [|
                {Id = 0;Tag = ""       ;Type =  "Pos.None"          ;Cod =  0   ;Russian =  ""                };
                {Id = 0;Tag = "сущ"    ;Type =  "Pos.Noun"          ;Cod =  1   ;Russian =  "Существительное" };        
                {Id = 0;Tag = "прл"    ;Type =  "Pos.Adjective"     ;Cod =  2   ;Russian =  "Прилагательное"  };
                {Id = 0;Tag = "гл"     ;Type =  "Pos.Verb"          ;Cod =  3   ;Russian =  "Глагол"          };
                {Id = 0;Tag = "нар"    ;Type =  "Pos.Adverb"        ;Cod =  4   ;Russian =  "Наречие"         };
                {Id = 0;Tag = "числ"   ;Type =  "Pos.Numeral"       ;Cod =  5   ;Russian =  "Числительное"    };
                {Id = 0;Tag = "прч"    ;Type =  "Pos.Participle"    ;Cod =  6   ;Russian =  "Причастие"       };
                {Id = 0;Tag = "дееп"   ;Type =  "Pos.Transgressive" ;Cod =  7   ;Russian =  "Деепричастие"    };
                {Id = 0;Tag = "мест"   ;Type =  "Pos.Pronoun"       ;Cod =  8   ;Russian =  "Местоимение"     };
                {Id = 0;Tag = "предл"  ;Type =  "Pos.Preposition"   ;Cod =  9   ;Russian =  "Предлог"         };
                {Id = 0;Tag = "союз"   ;Type =  "Pos.Conjunction"   ;Cod =  10  ;Russian =  "Союз"            };
                {Id = 0;Tag = "част"   ;Type =  "Pos.Particle"      ;Cod =  11  ;Russian =  "Частица"         };
                {Id = 0;Tag = "межд"   ;Type =  "Pos.Interjection"  ;Cod =  12  ;Russian =  "Междометие"      };
                {Id = 0;Tag = "предик" ;Type =  "Pos.Predicative"   ;Cod =  13  ;Russian =  "Предикатив"      };
                {Id = 0;Tag = "ввод"   ;Type =  "Pos.Parenthesis"   ;Cod =  14  ;Russian =  "Вводное слово"   }
        |]
        
        let GenderTagDataArr :MainTagDict array = [|
                {Id = 0;Tag = ""    ;Type =   "Gender.None"      ;Cod =   0 ;Russian =   ""        };
                {Id = 0;Tag = "муж" ;Type =   "Gender.Masculine" ;Cod =   1 ;Russian =   "Мужской" };
                {Id = 0;Tag = "жен" ;Type =   "Gender.Feminine"  ;Cod =   2 ;Russian =   "Женский" };
                {Id = 0;Tag = "ср"  ;Type =   "Gender.Neuter"    ;Cod =   3 ;Russian =   "Средний" };
                {Id = 0;Tag = "общ" ;Type =   "Gender.Common"    ;Cod =   4 ;Russian =   "Общий"   }
        |]
        
        let NumberTagDataArr :MainTagDict array = [|
                {Id = 0;Tag = ""   ;Type =    "Number.None"          ;Cod =    0 ;Russian =  ""};
                {Id = 0;Tag = "ед" ;Type =    "Number.Singular"      ;Cod =    1 ;Russian =  "Единственное" };
                {Id = 0;Tag = "мн" ;Type =    "Number.Plural"        ;Cod =    2 ;Russian =   "Множественное" }
        |]

        let CaseTagDataArr :MainTagDict array = [|
                {Id = 0;Tag = ""     ;Type =   "Case.None"          ;Cod =    0 ;Russian =  ""              };
                {Id = 0;Tag = "им"   ;Type =   "Case.Nominative"    ;Cod =    1 ;Russian =  "Именительный"  };
                {Id = 0;Tag = "род"  ;Type =   "Case.Genitive"      ;Cod =    2 ;Russian =  "Родительный"   };
                {Id = 0;Tag = "дат"  ;Type =   "Case.Dative"        ;Cod =    3 ;Russian =  "Дательный"     };
                {Id = 0;Tag = "вин"  ;Type =   "Case.Accusative"    ;Cod =    4 ;Russian =  "Винительный"   };
                {Id = 0;Tag = "тв"   ;Type =   "Case.Instrumental"  ;Cod =    5 ;Russian =  "Творительный"  };
                {Id = 0;Tag = "пр"   ;Type =   "Case.Prepositional" ;Cod =    6 ;Russian =  "Предложный"    };
                {Id = 0;Tag = "мест" ;Type =   "Case.Locative"      ;Cod =    7 ;Russian =  "Местный"       };
                {Id = 0;Tag = "парт" ;Type =   "Case.Partitive"     ;Cod =    8 ;Russian =  "Частичный"     };
                {Id = 0;Tag = "зват" ;Type =   "Case.Vocative"      ;Cod =    9 ;Russian =  "Звательный"    };
                {Id = 0;Tag = "счет" ;Type =   "Case.Counting"      ;Cod =   10 ;Russian =  "Cчетный"       }
        |]
        
        let TenseTagDataArr :MainTagDict array = [|
                {Id = 0;Tag = ""     ;Type =   "Tense.None"          ;Cod =     0;Russian =   ""           };
                {Id = 0;Tag = "прош" ;Type =   "Tense.Past"          ;Cod =     1;Russian =   "Прошедшее"  };
                {Id = 0;Tag = "наст" ;Type =   "Tense.Present"       ;Cod =     2;Russian =   "Настоящее"  };
                {Id = 0;Tag = "буд"  ;Type =   "Tense.Future"        ;Cod =     3;Russian =   "Будущее"    };
                {Id = 0;Tag = "инф"  ;Type =   "Tense.Infinitive"    ;Cod =     4;Russian =   "Инфинитив"  }                
        |]

        let PersonTagDataArr :MainTagDict array = [|
                {Id = 0;Tag = ""    ;Type =    "Person.None"   ;Cod =    0 ;Russian =   ""       };
                {Id = 0;Tag = "1-е" ;Type =    "Person.First"  ;Cod =    1 ;Russian =   "Первое" };
                {Id = 0;Tag = "2-е" ;Type =    "Person.Second" ;Cod =    2 ;Russian =   "Второе" };
                {Id = 0;Tag = "3-е" ;Type =    "Person.Third"  ;Cod =    3 ;Russian =   "Третие" }                   
        |]

        let AnimalTagDataArr :MainTagDict array = [|
                {Id = 0;Tag = ""     ;Type =    "Animal.None"          ;Cod =    0 ;Russian =  ""               };
                {Id = 0;Tag = "одуш" ;Type =    "Animal.Animated"      ;Cod =    1 ;Russian =  "Одушевленный"   };
                {Id = 0;Tag = "неод" ;Type =    "Animal.Inanimate"     ;Cod =    2 ;Russian =  "Неодушевленный" }
        |]

        
        let OverTagDataArr :OverTagDict array = [|
                {Id = 0;Bit = 0  ;Tag = "прев"     ;Type =      "Over.Superlative"     ;IntVal = 1L         ;Russian = "Превосходная"                 };
                {Id = 0;Bit = 1  ;Tag = "сравн"    ;Type =      "Over.Comparative"     ;IntVal = 2L         ;Russian = "Сравнительная"                };
                {Id = 0;Bit = 2  ;Tag = "крат"     ;Type =      "Over.Short"           ;IntVal = 4L         ;Russian = "Краткое"                      };
                {Id = 0;Bit = 3  ;Tag = "неизм"    ;Type =      "Over.Immutable"       ;IntVal = 8L         ;Russian = "Неизменяемое"                 };
                {Id = 0;Bit = 4  ;Tag = "сов"      ;Type =      "Over.Perfect"         ;IntVal = 16L        ;Russian = "Совершенный"                  };
                {Id = 0;Bit = 5  ;Tag = "несов"    ;Type =      "Over.Imperfect"       ;IntVal = 32L        ;Russian = "Несовершенный"                };
                {Id = 0;Bit = 6  ;Tag = "2вид"     ;Type =      "Over.PastSimple"      ;IntVal = 64L        ;Russian = "2 вид"                        };
                {Id = 0;Bit = 7  ;Tag = "непер"    ;Type =      "Over.Intransitive"    ;IntVal = 128L       ;Russian = "Непереходный"                 };
                {Id = 0;Bit = 8  ;Tag = "перех"    ;Type =      "Over.Transitive"      ;IntVal = 256L       ;Russian = "Переходный"                   };
                {Id = 0;Bit = 9  ;Tag = "воз"      ;Type =      "Over.Reflexive"       ;IntVal = 512L       ;Russian = "Возвратный"                   };
                {Id = 0;Bit = 10 ;Tag = "пер/не"   ;Type =      "Over.PerNot"          ;IntVal = 1024L      ;Russian = "Несовершенный и совершенный"  };    
                {Id = 0;Bit = 11 ;Tag = "безл"     ;Type =      "Over.Impersonal"      ;IntVal = 2048L      ;Russian = "Безличный"                    };    
                {Id = 0;Bit = 12 ;Tag = "пов"      ;Type =      "Over.Imperative"      ;IntVal = 4096L      ;Russian = "Повелительное наклонение"     };    
                {Id = 0;Bit = 13 ;Tag = "обст"     ;Type =      "Over.Circumstantial"  ;IntVal = 8192L      ;Russian = "Обстоятельственные"           };
                {Id = 0;Bit = 14 ;Tag = "опред"    ;Type =      "Over.Definitive"      ;IntVal = 16384L     ;Russian = "Определительные"              };
                {Id = 0;Bit = 15 ;Tag = "врем"     ;Type =      "Over.Time"            ;IntVal = 32768L     ;Russian = "Времени"                      };
                {Id = 0;Bit = 16 ;Tag = "места"    ;Type =      "Over.Places"          ;IntVal = 65536L     ;Russian = "Места"                        };
                {Id = 0;Bit = 17 ;Tag = "цель"     ;Type =      "Over.Goals"           ;IntVal = 131072L    ;Russian = "Цели"                         };
                {Id = 0;Bit = 18 ;Tag = "причин"   ;Type =      "Over.Reasons"         ;IntVal = 262144L    ;Russian = "Причины"                      };
                {Id = 0;Bit = 19 ;Tag = "кач"      ;Type =      "Over.Qualitative"     ;IntVal = 524288L    ;Russian = "Качественное"                 };   
                {Id = 0;Bit = 20 ;Tag = "степ"     ;Type =      "Over.Power"           ;IntVal = 1048576L   ;Russian = "Количественные меры и степени"};
                {Id = 0;Bit = 21 ;Tag = "вопр"     ;Type =      "Over.Interrogative"   ;IntVal = 2097152L   ;Russian = "Вопросительное"               };
                {Id = 0;Bit = 22 ;Tag = "напр"     ;Type =      "Over.Direction"       ;IntVal = 4194304L   ;Russian = "Направление"                  };
                {Id = 0;Bit = 23 ;Tag = "спос"     ;Type =      "Over.OfManner"        ;IntVal = 8388608L   ;Russian = "Способа"                      }; 
                {Id = 0;Bit = 24 ;Tag = "кол"      ;Type =      "Over.Quantitative"    ;IntVal = 16777216L  ;Russian = "Количественные"               };
                {Id = 0;Bit = 25 ;Tag = "поряд"    ;Type =      "Over.Ordinal"         ;IntVal = 33554432L  ;Russian = "Порядковые"                   };
                {Id = 0;Bit = 26 ;Tag = "неопр"    ;Type =      "Over.Indefinite"      ;IntVal = 67108864L  ;Russian = "Неопределенное"               }; 
                {Id = 0;Bit = 27 ;Tag = "собир"    ;Type =      "Over.Collective"      ;IntVal = 134217728L ;Russian = "Собирательные"                };
                {Id = 0;Bit = 28 ;Tag = "страд"    ;Type =      "Over.Passive"         ;IntVal = 268435456L ;Russian = "Страдательное"                };     
                {Id = 0;Bit = 29 ;Tag = "нар"      ;Type =      "Over.Adverb"          ;IntVal = 536870912L ;Russian = "Наречие"                      };
                {Id = 0;Bit = 30 ;Tag = "прл"      ;Type =      "Over.Adjective"       ;IntVal = 1073741824L;Russian = "Прилогательное"               };
                {Id = 0;Bit = 31 ;Tag = "сущ"      ;Type =      "Over.Noun"            ;IntVal = 2147483648L;Russian = "Существительное"              }
        |]        



module TagDataFunc =

        open CreateHagenMorphDb.DbModel
        open TagData
        
        let fillMainTagDb (table :string) (arr :MainTagDict array) :int option array =
                let r = ClearTable table
                if r >= 0 then
                        arr
                        |> Array.map (insertMainTagDict table)
                else
                        [|None|]
                
        let fillOverTagDb (arr :OverTagDict array) :int option array =
                let r = ClearTable "OverTagDict"
                if r >= 0 then
                        arr
                        |> Array.map insertOverTagDict
                else
                        [|None|]
                
        let fillTag =
                let res = [|
                        fillMainTagDb "AnimalTagDict" AnimalTagDataArr
                        fillMainTagDb "CaseTagDict" CaseTagDataArr 
                        fillMainTagDb "GenderTagDict" GenderTagDataArr
                        fillMainTagDb "NumberTagDict" NumberTagDataArr
                        fillMainTagDb "PersonTagDict" PersonTagDataArr
                        fillMainTagDb "PosTagDict" PosTagDataArr
                        fillMainTagDb "TenseTagDict" TenseTagDataArr
                
                        fillOverTagDb OverTagDataArr
                |]
                res