export const RegExps = {
  EMAIL_REGEX: '[A-Za-z0-9._%+\\-]{2,}@[a-zA-Z0-9\\-]{1,}([.]{1}[a-zA-Z0-9]{2,}|[.]{1}[a-zA-Z0-9\\-]{2,}[.]{1}[a-zA-Z\\-]{2,})|([.]{1}[a-zA-Z\\-]{2,}[.]{1}[a-zA-Z\\-]{2,})',
  PASSWORD_REGEX: '^(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z]).{6,}$',
  LATIN_NAMES_REGEX: '^[a-zA-Z, -]+$',
  NUMBER_REGEX: '^[0-9]+$',
  CYRILLIC_NAMES_REGEX: "^[аАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ, -.]+$",
  LATIN_AND_CYRILLIC_NAMES_REGEX: "^[A-Za-zА-Яа-я\\-\\s]+$",
  PHONE_NUMBER_REGEX: "^[0-9, +\\-]+$",
  LETTERS_AND_NUMBERS_REGEX: "^[A-Za-zаАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ,0-9, -.]+$",
  DIPLOMA_NUMBER_REGEX: "^[A-Za-zаАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ,0-9,-]+$",
  LATIN_AND_NUMBER_REGEX: "^[a-zA-Z,0-9\\/\\- ]+$",
  LATIN_AND_NUMBER_ONLY_REGEX: "^[a-zA-Z, 0-9]+$",
  CYRILLIC_AND_NUMBER_REGEX: "^[аАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ,0-9, -.]+$",
  LETTERS_NUMBERS_AND_SYMBOLS_REGEX: "^[A-Za-zаАбБвВгГдДеЕжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩьъЪюЮяЯ,0-9, -.'\":;]+$",
};

export const UserRoleAliases = {
  ADMINISTRATOR: 'administrator',
  MOSV: 'mosv',
  AUTHORIZED_ENTITY_ACTIVE: 'authorizedEntityActive',
  AUTHORIZED_ENTITY_PASSIVE: 'authorizedEntityPassive'
};

export const ContentTypes = {
  EXCEL: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
  PDF: 'application/pdf'
};