<script>
  import AppVariant from "$lib/components/AppVariant.svelte";
  import AppHeading from "$lib/components/AppHeading.svelte";
</script>

<AppHeading>Menu</AppHeading>
<div class="w-1/2 mx-auto flex flex-col gap-6">
  <a href="translations">
    <AppVariant>Translations</AppVariant>
  </a>
  <a href="quiz">
    <AppVariant>Quiz</AppVariant>
  </a>
</div>
