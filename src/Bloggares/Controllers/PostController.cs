using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

namespace Bloggares.Controllers
{
	public class Post
	{
		public Post(string slug, string title, string content)
		{
			Slug = slug;
			Title = title;
			Content = content;
		}

		public string Slug { get; }

		public string Title { get; }

		public string Content { get; }
	}

	[Route("api/posts")]
    public class PostController : Controller
    {
		// I hate regions, but this array's gonna exist here only for a short while, so fuck it ;)
		#region Fake data
		private readonly IEnumerable<Post> posts = new Post[] {
				new Post("test-post", "Test post number one", @"# Diva siste est resurgebant in fluit praedae

## Etruscam aptamque locus magna temporis Peleus

[Lorem markdownum](http://zeus.ugent.be/) deque **successit tempora passo**
nostras et gemitu, monstravit fecunda aquae quoniam, de mater Memnonis? Exercere
silentia nisi sanguine nomine ultra prioris, marmoris curis his caelestia venis,
abscidit. Ad sed violesque removit vero temptat semperque hoc **sumpsisse voce**
ferit, Priamum. Sua dilatus occupat leves meae cum cognovit, ad adfata; iam quam
eripiunt.

> Obitum corpus inerti Peleus ortos umectat, ne vectus. A pelagi tum experiar
> linguis circumvolat nocet coniugis flammae pervia
> [ille](http://heeeeeeeey.com/), tum fores? Remoliri nisi, arce labor Solem et
> tibi creatis undas pio in quibus fieri librata invidiosa fulgentem litus. Fuge
> dubitare violentia manus quodcumque et capiunt minister in morbi **formam**,
> Veneris promittit carmen, quem diu. Caerulus edidit altera opemque, a [quantum
> simul](http://reddit.com/r/thathappened) cadente data.

## Esset est inter me pronepos nos mota

Sed quae audito laetusque quaeque inulta Labitur annos: haud penetraret Thisbes
laevum Rhadamanthus Dicta, revulsum flos censuque. Dilatus diem verso [positi
quisque astra](http://www.raynelongboards.com/) rediit suo rursus avem
miserabile meus. Thisbe canibus [admittitur sine](http://www.metafilter.com/);
delata saxum est illos quidem. Eas sanguine, iuvencam animae
[ne](http://haskell.org/), neve caesis euntem; a. **Scopulum Nelea delubris** in
laeva; inter inque habet alumno umeris, magno ossa.

Flammae illum nefando, viae veste Hymenaeus in telo. Monilia et stupet terras,
et dabant nunc?

Ferens ductae, adfusique, illis et dicitur unica. Inficit est numen aris tempore
[his Gyaroque sumptus](http://www.wtfpl.net/) Ammon.

## Quibus tuum tuque a pariter nomina

**Generi** quod **lumine urbes**! Iram ipse et incumbens aera, parvo contra
perdis; demens Lyctia, ore. Minuunt fuerat! Est arvis adiuvat, litus positasque.
Videri dea, furor?

> Diu non dicor mora medii et igitur praeteritae *alebat vestigia* vetus ora
> adacto bacchantum hirsuti ablata, cara? Tamen *et iussit*, sum quae corpora,
> signa digiti: non facta. Quisquis et tam, et ita aut iphis, moenia, removit
> bellum unus terrent impune baculo. Adspicias et Amuli Aeacidae summa vobisque
> vinclo: spem clavigeram caelarat, aevum **thalamosque illa vertere**, et. Ut
> turbatis letique cum Titan aeacides ardua tibi conscia, futuri.

Nova properas cantat; et munera fortisque novo pontus cupidine. Ponderibus casus
feritate fortis diva humum depulerant riget, unde recludi sit subit. Caietam
bimari pondere tonitruque quae, deus mutilatae et status quia. Non velut modo
validum ipse possunt sacra superat lacrimas. Nebulas elususque terribilis urbem.

Adsuetos fors fugiant miserque inultam mortalia sibi qua quam mihi.
[Adversos](http://seenly.com/) poterat arentia funis umbra sed vestigia, quid
quis membra Finierat threicius ostendens sorores. Ruit gutture restat auras
huius et, [non manat falsisque](http://imgur.com/) partes Rhamnusidis fonti
Phoronide vestras nurus. Aere iam umeris est quod est decusque Somnia sistrorum
prius lacrimabile taedas praecingitur."),
				new Post("test-post-2", "Test post number two", ""),
				new Post("test-post-3", "Test post number three", ""),
			};
		#endregion

		[HttpGet]
        public IEnumerable<Post> Index()
        {
			return posts;
        }

		[HttpGet("{slug}")]
		public object Single(string slug) {
			return posts
				.Where(x => x.Slug == slug)
				.Single();
		}
    }
}
