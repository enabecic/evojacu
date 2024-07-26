export interface KategorijaPreuzmiResponse {
  kategorije: KategorijaPreuzmiResponseKategorija[];
}

export interface KategorijaPreuzmiResponseKategorija {
  kategorijaID: number;
  naziv: string;
}
