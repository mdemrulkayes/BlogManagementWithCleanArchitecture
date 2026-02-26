import {
  Component,
  OnInit,
  inject,
  signal,
  ElementRef,
  ViewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  map,
  startWith,
  debounceTime,
  distinctUntilChanged,
} from 'rxjs/operators';
import { COMMA, ENTER } from '@angular/cdk/keycodes';

// Material Imports
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule, MatChipInputEvent } from '@angular/material/chips';
import {
  MatAutocompleteModule,
  MatAutocompleteSelectedEvent,
} from '@angular/material/autocomplete';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonToggleModule } from '@angular/material/button-toggle'; // Added for the toggle switch

// Markdown Import
import { MarkdownModule, MarkdownService } from 'ngx-markdown';
import {
  CategoryResponse,
  CategoryService,
  PostService,
  TagResponse,
  TagService,
} from '../../../../client';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-create-update',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCardModule,
    MatIconModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatButtonToggleModule,
    MarkdownModule,
  ],
  templateUrl: './create-update.html',
  styleUrl: './create-update.scss',
})
export class CreateUpdate implements OnInit {
  private fb = inject(FormBuilder);
  private readonly postService = inject(PostService);
  private readonly categoryService = inject(CategoryService);
  private readonly tagService = inject(TagService);
  // private readonly markdownService = inject(MarkdownService);

  @ViewChild('editorInput') editorInput!: ElementRef<HTMLTextAreaElement>;

  form!: FormGroup;
  isLoading = signal<boolean>(false);

  // UI State
  isPreviewMode = signal<boolean>(false); // Controls Write vs Preview view

  // Data Signals
  categories = signal<CategoryResponse[]>([]);
  allTags = signal<TagResponse[]>([]);
  filteredTags = signal<TagResponse[]>([]);
  selectedTags = signal<TagResponse[]>([]);

  postStatuses = Object.values(PostStatus);
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor() {
    this.initForm();
    this.setupSlugGenerator();
    this.setupTagFiltering();
  }

  ngOnInit(): void {
    this.loadInitialData();
  }

  private initForm(): void {
    this.form = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(5)]],
      text: ['', [Validators.required]],
      slug: [
        '',
        [Validators.required, Validators.pattern('^[a-z0-9]+(?:-[a-z0-9]+)*$')],
      ],
      status: [PostStatus.DRAFT, Validators.required],
      categoryIds: [[]],
      tagCtrl: [''],
    });
  }

  async loadInitialData() {
    this.isLoading.set(true);
    try {
      const [cats, tags] = await Promise.all([
        firstValueFrom(this.categoryService.getCategories()),
        firstValueFrom(this.tagService.getAllTags()),
      ]);

      this.categories.set(cats.items || []);
      this.allTags.set(tags.items || []);
      this.filteredTags.set(tags.items?.slice() || []);
    } catch (error) {
      console.error('Failed to load data', error);
    } finally {
      this.isLoading.set(false);
    }
  }

  // --- Toolbar Logic ---
  insertFormatting(
    format: 'bold' | 'italic' | 'heading' | 'link' | 'code' | 'quote' | 'list',
  ): void {
    if (this.isPreviewMode()) return; // Disable toolbar in preview mode

    const textarea = this.editorInput.nativeElement;
    const start = textarea.selectionStart;
    const end = textarea.selectionEnd;
    const text = this.form.get('text')?.value || '';
    const selectedText = text.substring(start, end);

    let replacement = '';

    switch (format) {
      case 'bold':
        replacement = `**${selectedText || 'bold text'}**`;
        break;
      case 'italic':
        replacement = `*${selectedText || 'italic text'}*`;
        break;
      case 'heading':
        replacement = `\n## ${selectedText || 'Heading'}\n`;
        break;
      case 'link':
        replacement = `[${selectedText || 'link text'}](url)`;
        break;
      case 'code':
        replacement = `\`\`\`typescript\n${selectedText || '// code here'}\n\`\`\``;
        break;
      case 'quote':
        replacement = `\n> ${selectedText || 'quote'}\n`;
        break;
      case 'list':
        replacement = `\n- ${selectedText || 'list item'}`;
        break;
    }

    const newText =
      text.substring(0, start) + replacement + text.substring(end);
    this.form.get('text')?.setValue(newText);

    // Defer focus slightly to ensure DOM update
    setTimeout(() => {
      textarea.focus();
      textarea.setSelectionRange(
        start + replacement.length,
        start + replacement.length,
      );
    });
  }

  // --- Slug & Tag Logic ---
  private setupSlugGenerator(): void {
    const title = this.form.get('title');
    const slug = this.form.get('slug');
    title?.valueChanges
      .pipe(debounceTime(300), distinctUntilChanged())
      .subscribe((val) => {
        if (val && !slug?.dirty) slug?.setValue(this.slugify(val));
      });
  }

  private slugify(text: string): string {
    return text
      .toString()
      .toLowerCase()
      .trim()
      .replace(/\s+/g, '-')
      .replace(/[^\w\-]+/g, '')
      .replace(/\-\-+/g, '-');
  }

  private setupTagFiltering(): void {
    this.form
      .get('tagCtrl')
      ?.valueChanges.pipe(
        startWith(null),
        map((tagInput: string | null) =>
          tagInput ? this._filterTags(tagInput) : this.allTags().slice(),
        ),
      )
      .subscribe((filtered) => this.filteredTags.set(filtered));
  }

  addTag(event: MatChipInputEvent): void {
    event.chipInput!.clear();
    this.form.get('tagCtrl')?.setValue(null);
  }
  removeTag(t: TagResponse): void {
    this.selectedTags.update((tags) =>
      tags.filter((tag) => tag.tagId !== t.tagId),
    );
  }
  selectedTag(e: MatAutocompleteSelectedEvent): void {
    const newTag = e.option.value;
    if (!this.selectedTags().find((t) => t.tagId === newTag.tagId))
      this.selectedTags.update((ts) => [...ts, newTag]);
    this.form.get('tagCtrl')?.setValue(null);
    // Manual reset for the input element
    const input = document.getElementById('tagInput') as HTMLInputElement;
    if (input) input.value = '';
  }
  private _filterTags(v: any): TagResponse[] {
    const filter = (typeof v === 'string' ? v : v.name).toLowerCase();
    return this.allTags().filter((tag) =>
      tag.name.toLowerCase().includes(filter),
    );
  }

  get tagControl(): FormControl {
    return this.form.get('tagCtrl') as FormControl;
  }

  onSubmit() {
    if (this.form.invalid) return;
    console.log('Markdown Payload:', this.form.value);
  }

  togglePreviewMode(value: boolean): void {
    this.isPreviewMode.set(value);
    // this.markdownService.reload();
  }
}

export enum PostStatus {
  DRAFT = 'DRAFT',
  PUBLISHED = 'PUBLISHED',
  ABANDONED = 'ABANDONED',
}
